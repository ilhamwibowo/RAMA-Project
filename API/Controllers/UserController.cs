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
    public class UserController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountController> _logger;
        public UserController(DataContext context, ITokenService tokenService, ILogger<AccountController> logger)
        {
            _tokenService = tokenService;
            _context = context;
            _logger = logger;
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDto>> getProfile(int id) {
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == id);

            if(user == null) {
                return BadRequest("User not found");
            }
            return Ok(new ProfileDto{
                // Email = user.Email,
                Role = user.Role,
                KTP = user.KTP,
                No_HP = user.No_HP,
                Birthday = user.Birthday
            });

        
        
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult> editProfile(int id, ProfileDto profile) {
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == id);

            if (user == null) {
                return BadRequest("User not found");
            }

            //Profile found 
            user.Role = profile.Role;
            user.KTP = profile.KTP;
            user.No_HP = profile.No_HP;
            user.Birthday = profile.Birthday;

            try {
                _context.Accounts.Update(user);
                await _context.SaveChangesAsync();
            }
            catch(Exception e) {
                _logger.LogError(e, "Failed to save user");
                return BadRequest("Failed to save user");
            }

            return Ok("Success");
            



        }

    }
}