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
        public AccountController(DataContext context, ITokenService tokenService, ILogger<AccountController> logger)
        {
            _tokenService = tokenService;
            _context = context;
            _logger = logger;
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
                PasswordSalt = hmac.Key
            };

            _context.Account.Add(user);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Task User creation completed");
                return new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to save user");
                return BadRequest("Failed to save user");
            }
        }

        // Login Endpoint here

        // Buat DTO Login isinya sama kaya register, tapi dibedain aja karena fungsinya beda.
        // Cari User dengan, var user = _context.Account.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
        // passwordnya dengan di hash dulu, baru dibandingin. contoh
        // using var hmac = new HMACSHA512 (user.PasswordSalt);
        // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        // atau boleh buat repository untuk Account.

        
        private async Task<bool> AccountExists(string Email)
        {
            return await _context.Account.AnyAsync(x => x.Email == Email);
        }
        private bool EmailValid(string Email)
        {
            //check Email for valid using regex
            string pattern = @"^[\w-]+@([\w-]+\.)+[\w-]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(Email);
        }
    }
}