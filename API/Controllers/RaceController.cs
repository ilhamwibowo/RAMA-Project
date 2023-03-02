using API.Data;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Get participant of a specific race
        // Page and pageSize required in query
        [AllowAnonymous]
        [HttpGet("{raceId}/attendees")]
        public async Task<IActionResult> GetRaceAttendance(int raceId, int page = 1, int pageSize = 10)
        {
            try
            {
                // Get race as per raceId
                var race = await _context.Races
                    .AsNoTracking()
                    .Where(r => r.RaceId == raceId)
                    .Include(r => r.RaceAttendee)
                    .ThenInclude(ra => ra.Runner)
                    .FirstOrDefaultAsync();
                
                // Race not found
                if (race == null)
                {
                    return NotFound();
                }
                
                // Get race attendance
                var raceAttendance = race.RaceAttendee
                    .OrderBy(ra => ra.Position)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(ra => new RaceAttendanceDto
                    {
                        ProfilePhotoUrl = ra.Runner.ProfilePhoto?.Url,
                        Name = ra.Runner.Name,
                        BibNumber = ra.BibNumber,
                        Position = ra.Position,
                        Duration = ra.Duration,
                        FinishTime = ra.FinishTime
                    })
                    .ToList();

                return Ok(raceAttendance);
            } 
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }

        }


        // Get all race attendance without pagination
        // For testing purposes, maybe necessary
        [AllowAnonymous]
        [HttpGet("{raceId}/attendance")]
        public async Task<IActionResult> GetAllRaceAttendance(int raceId)
        {
            try
            {
                // Get race as per raceId
                var race = await _context.Races
                    .AsNoTracking()
                    .Where(r => r.RaceId == raceId)
                    .Include(r => r.RaceAttendee)
                    .ThenInclude(ra => ra.Runner)
                    .FirstOrDefaultAsync();
                
                // Race not found
                if (race == null)
                {
                    return NotFound();
                }
                
                // Get race attendance
                var raceAttendance = race.RaceAttendee
                    .OrderBy(ra => ra.Position)
                    .Select(ra => new RaceAttendanceDto
                    {
                        ProfilePhotoUrl = ra.Runner.ProfilePhoto?.Url,
                        Name = ra.Runner.Name,
                        BibNumber = ra.BibNumber,
                        Position = ra.Position,
                        Duration = ra.Duration,
                        FinishTime = ra.FinishTime
                    })
                    .ToList();

                return Ok(raceAttendance);
            } 
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        } 

        // Get race details including photos, name, etc
        // For testing purposes
        [HttpGet("{raceId}/details")]
        public async Task<ActionResult<RaceDto>> GetRaceDetails(int raceId)
        {
            var race = await _context.Races
                .Include(r => r.RaceAttendee)
                .ThenInclude(ra => ra.Runner)
                .Include(r => r.RaceAlbum)
                .FirstOrDefaultAsync(r => r.RaceId == raceId);

            if (race == null)
            {
                return NotFound();
            }

            var raceDetails = new RaceDto
            {
                RaceId = race.RaceId,
                RaceName = race.RaceName,
                RaceAttendee = race.RaceAttendee,
                RaceAlbum = race.RaceAlbum
            };

            return Ok(raceDetails);
        }

    }
}