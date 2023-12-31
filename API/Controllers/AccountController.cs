using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailService _emailService;
        public AccountController(DataContext context, ITokenService tokenService, ILogger<AccountController> logger, IEmailService emailService)
        {
            _tokenService = tokenService;
            _context = context;
            _logger = logger;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            using var hmac = new HMACSHA512();
            string Email = registerDto.Email.ToLower();
            string password = registerDto.Password;
            if (await AccountExists(Email))
                return BadRequest("Email is Registered");
            if (!EmailValid(Email))
                return BadRequest("Invalid Email");

            var user = new Account
            {
                Email = Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key,
                Role = "Runner"
            };

            _context.Accounts.Add(user);
            
            await _context.SaveChangesAsync();

            return Ok
            (
                new UserDto
                    {
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user),
                        Role = user.Role
                    }
            );
        }

        // Login Endpoint here

        // Buat DTO Login isinya sama kaya register, tapi dibedain aja karena fungsinya beda.
        // Cari User dengan, var user = _context.Account.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
        // passwordnya dengan di hash dulu, baru dibandingin. contoh
        // using var hmac = new HMACSHA512 (user.PasswordSalt);
        // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        // atau boleh buat repository untuk Account.
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
            string email = loginDto.Email.ToLower();
            string password = loginDto.Password;
            if (!(await AccountExists(email)))
                return BadRequest("Email is not registered");
            // how to get email and password from database? belum tau yang ini caranya gimana
            Account user = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            using var hmac = new HMACSHA512 (user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != user.PasswordHash[i])
                    return BadRequest("Password does not match");
            return Ok(new UserDto {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Role = user.Role
                
            });
        } 
        
        [HttpPut("forgotpassword/requestotp")]
        public async Task<ActionResult> RequestOTP(string Email)
        {
            var user = await _context.Accounts.Select(x => new {x.Email}).FirstOrDefaultAsync(x => x.Email == Email);
            if (user == null)
                return BadRequest("Account not found");
            var forgotPasswordHistory = await _context.ForgotPasswordHistories.FirstOrDefaultAsync(x => x.Email == Email && x.Date == DateOnly.FromDateTime(DateTime.UtcNow));
            if (forgotPasswordHistory != null)
            {
                if (forgotPasswordHistory.RequestCount >= 3)
                    return BadRequest("You have reached the maximum number of OTP request");
                forgotPasswordHistory.RequestCount++;
                forgotPasswordHistory.Otp = GenerateOTP();
                await _context.SaveChangesAsync();
                _logger.LogInformation(forgotPasswordHistory.Otp);
                _logger.LogInformation(forgotPasswordHistory.Email);
                await _emailService.SendEmailAsync(
                    new MailDto
                    {
                        ToEmail = Email,
                        Subject = "Forgot Password OTP",
                        Body = $"Your OTP is {forgotPasswordHistory.Otp}"
                    }
                );
                return NoContent();
            }
            else {
                ForgotPasswordHistory fph = new ForgotPasswordHistory
                {
                    Email = Email,
                    Otp = GenerateOTP(),
                    RequestCount = 1,
                    CheckCount = 0
                };
                _context.ForgotPasswordHistories.Add(fph);
                await _context.SaveChangesAsync();
                await _emailService.SendEmailAsync(
                    new MailDto
                    {
                        ToEmail = Email,
                        Subject = "Forgot Password OTP",
                        Body = $"Your OTP is {fph.Otp}"
                    }
                );
                return NoContent();
            }
        }

        [HttpPut("forgotpassword/changepassword")]
        public async Task<ActionResult> ChangePasswordOtp(ForgotPasswordDto fpDto){
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == fpDto.Email);
            if (user == null)
                return BadRequest("Account not found");
            var fph = await _context.ForgotPasswordHistories.FirstOrDefaultAsync(x => x.Email == fpDto.Email && x.Date == DateOnly.FromDateTime(DateTime.UtcNow));
            if (fph == null)
                return BadRequest("No Forgot Password Request Found");
            if (fph.CheckCount >= 3)
                return BadRequest("You have reached the maximum attempt to change password. Try again tomorrow");
            if (fpDto.Otp == fph.Otp){
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(fpDto.NewPassword));
                user.PasswordSalt = hmac.Key;
                _context.Accounts.Update(user);
                _context.ForgotPasswordHistories.Remove(fph); //delete so it cant be used again
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else{
                fph.CheckCount++;
                _context.ForgotPasswordHistories.Update(fph);
                await _context.SaveChangesAsync();
                return BadRequest("Otp is incorrect");
            }
        }
        private async Task<bool> AccountExists(string Email)
        {
            return await _context.Accounts.AnyAsync(x => x.Email == Email);
        }
        private bool EmailValid(string Email)
        {
            //check Email for valid using regex
            string pattern = @"^[\w-]+@([\w-]+\.)+[\w-]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(Email);
        }

        private string GenerateOTP()
        {
            Random random = new Random();
            string otp = "";
            for (int i = 0; i < 6; i++)
            {
                otp += random.Next(0, 10);
            }
            return otp;
        }

    }
}