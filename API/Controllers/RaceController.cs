using API.Data;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API.Controllers
{
    [Authorize]
    public class RaceController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public RaceController(DataContext context, ILogger<AccountController> logger, IPhotoService photoService, IMapper mapper)
        {
            _photoService = photoService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        } 
        [HttpPost]
        public async Task<ActionResult> CreateRace([FromForm] RaceEditDto raceDto)
        {   
            _logger.LogInformation("asdsd");
            Account requester = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));
            // if (requester.Role != "Admin") Unauthorized("No Permission!");
            Race race = new Race 
            {
                RaceName = raceDto.RaceName,
                RaceDesc = raceDto.RaceDesc,
                RaceAlbum = new Album { AlbumName = raceDto.RaceName},
                StartTime = raceDto.StartTime,
                StartLocation = raceDto.StartLocation,
                Distance = raceDto.Distance,
                RegistrationFee = raceDto.RegistrationFee,
                Points = raceDto.Points,
                IsPublished = raceDto.IsPublished,
                StartDateRegistration = raceDto.StartDateRegistration,
                EndDateRegistration = raceDto.EndDateRegistration,
            };
            if (raceDto.photoFile != null)
            {
                if (!IPhotoService.isImage(raceDto.photoFile.FileName)) return BadRequest("File must be an Image");
                var photoUpload = await _photoService.AddPhotoAlbumAsync(raceDto.photoFile, "RaceThumbnails");
                if (photoUpload.Error != null) return BadRequest(photoUpload.Error.Message);

                race.RaceThumbnail = new Photo
                {
                    Url = photoUpload.SecureUrl.AbsoluteUri,
                    PublicId = photoUpload.PublicId
                };
            }

            await _context.Races.AddAsync(race);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRace), new { Id = race.RaceId}, _mapper.Map<RaceDto>(race));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<RaceDto>> GetAllRace()
        {
            List<Race> races = await _context.Races.Include(x => x.StartLocation).Include(p => p.RaceThumbnail).ToListAsync();

            if (races == null) return NotFound();

            return Ok
                (
                    new 
                    {
                        Races = _mapper.Map<IEnumerable<RaceDto>>(races),
                        Length = races.Count
                    }
                );
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<RaceDto>> GetRace(Guid id)
        {
            Race race = await _context.Races.Include(r => r.StartLocation ).Include(r => r.RaceAlbum).Include(p => p.RaceThumbnail).Include(p => p.Points).FirstOrDefaultAsync(x => x.RaceId.Equals(id));
            if (race == null) return NotFound();

            return Ok(_mapper.Map<RaceDto>(race));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRace(Guid id, [FromForm] RaceEditDto raceDto)
        {
            var requester = await _context.Accounts.Select(x => new { x.AccId, x.Role }).FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));
            if (requester.Role != "Admin") Unauthorized("No Permission!");

            Race race = _context.Races.FirstOrDefault(x => x.RaceId.Equals(id));
            Album newalbum = await _context.Albums.FirstOrDefaultAsync(x => x.AlbumId.Equals(raceDto.AlbumId));
            if (race == null) return NotFound();

            race.RaceName = raceDto.RaceName;
            race.RaceDesc = raceDto.RaceDesc;
            race.StartTime = raceDto.StartTime;
            race.StartLocation = raceDto.StartLocation;
            race.Distance = raceDto.Distance;
            race.RegistrationFee = raceDto.RegistrationFee;
            race.StartDateRegistration = raceDto.StartDateRegistration;
            race.EndDateRegistration = raceDto.EndDateRegistration;
            race.IsPublished = raceDto.IsPublished;
            race.Points = raceDto.Points;

            if (newalbum != null) race.RaceAlbum = newalbum;
            if (raceDto.photoFile != null)
            {
                if (!IPhotoService.isImage(raceDto.photoFile.FileName)) return BadRequest("File must be an Image");
                var photoUpload = await _photoService.AddPhotoAsync(raceDto.photoFile);
                if (photoUpload.Error != null) return BadRequest(photoUpload.Error.Message);

                race.RaceThumbnail = new Photo
                {
                    Url = photoUpload.SecureUrl.AbsoluteUri,
                    PublicId = photoUpload.PublicId
                };
            }
            _context.Races.Update(race);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<RaceDto>(race));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRace(Guid id)
        {
            var requester = await _context.Accounts.Select(x => new { x.AccId }).FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));
            // if (requester.Role != "Admin") Unauthorized("No Permission!");

            Race race = await _context.Races.FirstOrDefaultAsync(x => x.RaceId.Equals(id));
            if (race == null) return NotFound();

            _context.Races.Remove(race);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Get participant of a specific race
        // Page and pageSize required in query
        [AllowAnonymous]
        [HttpGet("{raceId}/attendees")]
        public async Task<ActionResult<RaceDto>> GetRaceAttendance(Guid raceId, int page = 1, int pageSize = 10)
        {
            // Get race as per raceId
            var race = await _context.Races
                .AsNoTracking()
                .Where(r => r.RaceId.Equals(raceId))
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

            return Ok( new LeaderboardDto
                {
                    RaceName = race.RaceName, 
                    RaceAttendee = raceAttendance,
                    TotalCount = raceAttendance.Count,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalPages = (int)(raceAttendance.Count / pageSize)
                });

        }


        // Get all race attendance without pagination
        // For testing purposes, maybe necessary
        [AllowAnonymous]
        [HttpGet("{raceId}/attendance")]
        public async Task<ActionResult<RaceDto>> GetAllRaceAttendance(Guid raceId)
        {
            // Get race as per raceId
            var race = await _context.Races
                .AsNoTracking()
                .Where(r => r.RaceId.Equals(raceId))
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


    }
}