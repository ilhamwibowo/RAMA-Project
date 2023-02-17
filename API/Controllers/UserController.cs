using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountController> _logger;
        private readonly IPhotoService _photoService;
        public UserController(DataContext context, ITokenService tokenService, ILogger<AccountController> logger, IPhotoService photoService)
        {
            _photoService = photoService;
            _tokenService = tokenService;
            _context = context;
            _logger = logger;
        } 

        [HttpGet]
        public async Task<ActionResult<ProfileDto>> getProfile() {
            // Get user based on id
            int id = User.GetUserId();
            var user = await _context.Accounts.Include(p => p.ProfilePhoto).FirstOrDefaultAsync(x => x.AccId == id);

            if(user == null) {
                return BadRequest("User not found");
            }
            //return user profile
            return Ok(new ProfileDto{
                Email = user.Email,
                Role = user.Role,
                KTP = user.KTP,
                No_HP = user.No_HP,
                Birthday = user.Birthday,
                ProfilePhoto = new PhotoDto{
                    Id = user.ProfilePhoto.PhotoId,
                    Url = user.ProfilePhoto.Url
                }
            });
        }

        [HttpPut("edit")]
        public async Task<ActionResult> editProfile(ProfileDto profile) {
            int id = User.GetUserId();
            // Get user based on id
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == id);

            if (user == null) {
                return BadRequest("User not found");
            }

            // TODO : Validate data using regular expression
            // Profile found 
            user.Role = profile.Role;
            user.KTP = profile.KTP;
            user.No_HP = profile.No_HP;
            user.Birthday = profile.Birthday;

            // Update user profile
            try {
                _context.Accounts.Update(user);
                await _context.SaveChangesAsync();
            }
            // Update fails
            catch(Exception e) {
                _logger.LogError(e, "Failed to save user");
                return BadRequest("Failed to save user");
            }

            // Update success
            return Ok("Success");
            
        }
        [HttpPost("add-photo")]
        public async Task<ActionResult> AddPhoto(IFormFile file)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == User.GetUserId());
            if (user == null) return Unauthorized("User not found");
            if (file == null) return BadRequest("No file");
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            user.ProfilePhoto = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            _context.Accounts.Update(user);
            int id = await _context.SaveChangesAsync();
            return Ok();

        }
    }
}