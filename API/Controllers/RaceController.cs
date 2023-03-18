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
        [HttpPost]
        public async Task<ActionResult<RaceDto>> CreateRace(RaceDto raceDto)
        {   
            Account requester = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == User.GetUserId());
            // if (requester.Role != "Admin") Unauthorized("No Permission!");
            Race race = new Race
                {
                    RaceName = raceDto.RaceName,
                    StartTime = raceDto.StartTime,
                    StartLocation = raceDto.StartLocation,
                    Distance = raceDto.Distance,
                    RegistrationFee = raceDto.RegistrationFee
                };
            await _context.Races.AddAsync(race);
            try {
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                _logger.LogError(e, "Failed to save Race");
                return BadRequest("Failed to save Race");
            }
            return CreatedAtAction(nameof(GetRace), new { Id = race.RaceId}, raceDto);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<RaceDto>> GetRace(int id)
        {
            Race race = await _context.Races.FirstOrDefaultAsync(x => x.RaceId == id);
            if (race == null) return NotFound();

            return Ok
                (
                    new RaceDto 
                        {
                            RaceName = race.RaceName,
                            StartTime = race.StartTime,
                            StartLocation = race.StartLocation,
                            Distance = race.Distance,
                            RegistrationFee = race.RegistrationFee,
                            Points = race.Points

                        }
                );
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRace(int id, RaceDto raceDto)
        {
            Account requester = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == User.GetUserId());
            // if (requester.Role != "Admin") Unauthorized("No Permission!");

            Race race = _context.Races.FirstOrDefault(x => x.RaceId == id);
            if (race == null) return NotFound();

            race.RaceName = raceDto.RaceName;
            race.StartTime = raceDto.StartTime;
            race.StartLocation = raceDto.StartLocation;
            race.Distance = raceDto.Distance;
            race.RegistrationFee = raceDto.RegistrationFee;

            _context.Races.Update(race);
            try 
                {
                    await _context.SaveChangesAsync();
                }
            catch (Exception e)
                {
                    _logger.LogError(e, "Failed to save Race");
                    return BadRequest("Failed to save Race");
                }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRace(int id)
        {
            Account requester = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == User.GetUserId());
            // if (requester.Role != "Admin") Unauthorized("No Permission!");

            Race race = await _context.Races.FirstOrDefaultAsync(x => x.RaceId == id);
            if (race == null) return NotFound();

            _context.Races.Remove(race);

            try 
                {
                    await _context.SaveChangesAsync();
                }
            catch (Exception e)
                {
                    _logger.LogError(e, "Failed to delete Race");
                    return BadRequest("Failed to delete Race");
                }
            return Ok();
        }

        // Get participant of a specific race
        // Page and pageSize required in query
        [AllowAnonymous]
        [HttpGet("{raceId}/attendees")]
        public async Task<ActionResult<RaceDto>> GetRaceAttendance(int raceId, int page = 1, int pageSize = 10)
        {
            try
            {
                // Get race as per raceId
                var race = await _context.Races
                    .AsNoTracking()
                    .Where(r => r.RaceId == raceId)
                    .Include(r => r.RaceAttendee)
                    .ThenInclude(ra => ra.Runner.ProfilePhoto)
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
                var totalCount = race.RaceAttendee.Count();

                return Ok( new{
                    RaceName = race.RaceName, 
                    RaceAttendee = raceAttendance,
                    TotalCount = totalCount,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                     });
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
        public async Task<ActionResult<RaceDto>> GetAllRaceAttendance(int raceId)
        {
            try
            {
                // Get race as per raceId
                var race = await _context.Races
                    .AsNoTracking()
                    .Where(r => r.RaceId == raceId)
                    .Include(r => r.RaceAttendee)
                    .ThenInclude(ra => ra.Runner.ProfilePhoto)
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

                return Ok( new RaceDto {
                     RaceName = race.RaceName, 
                     RaceAttendee = raceAttendance
                     });
            } 
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        } 


    }
}