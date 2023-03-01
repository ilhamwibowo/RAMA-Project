using API.Data;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class RaceController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IPhotoService _photoService;
        public RaceController(DataContext context, ILogger<AccountController> logger, IPhotoService photoService)
        {
            _photoService = photoService;
            _context = context;
            _logger = logger;
        } 
        //For Testing
        [HttpPost]
        public async Task<ActionResult> CreateRace()
        {   
            Race race = new Race
                {
                    RaceName = "Test",
                    RaceAlbum = new Album {
                        AlbumName = "Test"
                    }
                };
            await _context.Races.AddAsync(race);
            try {
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                _logger.LogError(e, "Failed to save Race");
                return BadRequest("Failed to save Race");
            }
            return Ok(race);
        }
    }
}