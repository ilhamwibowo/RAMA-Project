using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountController> _logger;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        public UserController(DataContext context, ITokenService tokenService, ILogger<AccountController> logger, IPhotoService photoService, IMapper mapper)
        {
            _photoService = photoService;
            _tokenService = tokenService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        } 

        [HttpGet]
        public async Task<ActionResult<ProfileDto>> getProfile() {
            // Get user based on id
            Guid id = User.GetUserId();
            var user = await _context.Accounts.Include(p => p.ProfilePhoto).FirstOrDefaultAsync(x => x.AccId.Equals(id));

            if(user == null) {
                return BadRequest("User not found");
            }
            
            //return user profile
            return Ok(_mapper.Map<ProfileDto>(user));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDto>> getProfile(Guid id) {
            // Get user based on id
            var requester = await _context.Accounts.Select(x => new { x.AccId, x.Role }).FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));
            if (requester.Role != "Admin") Unauthorized("No Permission!");

            var user = await _context.Accounts.Include(p => p.ProfilePhoto).FirstOrDefaultAsync(x => x.AccId.Equals(id));

            if(user == null) {
                return BadRequest("User not found");
            }
            
            //return user profile
            return Ok(_mapper.Map<ProfileDto>(user));
        }

        [HttpPut("edit")]
        public async Task<ActionResult> editProfile(ProfileDto profile) {
            Guid id = User.GetUserId();
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId.Equals(id));

            if (user == null) {
                return BadRequest("User not found");
            }

            // TODO : Validate data using regular expression
            // Profile found 
            if (profile.No_HP != null) {
                string patternHP = @"^(?:\+62|0)[1-9][0-9]{0,11}$";
                if (!Regex.IsMatch(profile.No_HP, patternHP)) {
                    return BadRequest("Invalid phone number");
                }
                user.No_HP = profile.No_HP;
            }

            if (profile.KTP != null) {
                string patternKTP = @"^[1-9]\d{15}$";
                if (!Regex.IsMatch(profile.KTP, patternKTP)) {
                    return BadRequest("Invalid KTP");
                }
                var checkUser = await _context.Accounts.FirstOrDefaultAsync(x => x.KTP == profile.KTP);
                if (checkUser != null && !checkUser.AccId.Equals(id)) {            
                    return BadRequest("KTP already registered");
                }
                user.KTP = profile.KTP;
            }

            if (profile.Role != null) {
                user.Role = profile.Role;
            }

            if (profile.Birthday != null) {
                user.Birthday = profile.Birthday;
            }
            if (profile.Name != null) {
                user.Name = profile.Name;
            }
            // Update user profile
            _context.Accounts.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
            
        }
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));
            if (user == null) return Unauthorized("User not found");
            if (file == null) return BadRequest("No file");
            if (!IPhotoService.isImage(file.FileName)) return BadRequest("File must be an Image");
            var result = await _photoService.AddPhotoAsync(file);
            
            if (result.Error != null) return BadRequest(result.Error.Message);

            user.ProfilePhoto = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            _context.Accounts.Update(user);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<PhotoDto>(user.ProfilePhoto));

        }
        
    }
}