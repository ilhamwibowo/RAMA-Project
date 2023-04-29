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
    public class CheckPointController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<AccountController> _logger;

        public CheckPointController(DataContext context, ITokenService tokenService, ILogger<AccountController> logger, IEmailService emailService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("scan/{ScannerId}")]
        public async Task<ActionResult> PostCheckPointData(string ScannerId, CheckPointDto cpd){
            Location l = await _context.Locations.FirstOrDefaultAsync(x => x.ScannerId == ScannerId);
            RaceAttendance ra = await _context.RaceAttendances.Include(x => x.CheckPoints).Include(x => x.Runner).FirstOrDefaultAsync(x => x.RaceId.Equals(cpd.RaceId) && x.RFID == cpd.RFID);
            if (l == null || ra == null) return NotFound();
            if (ra.CheckPoints.Any( x => x.ScannerId == ScannerId)) return Ok();
            ra.CheckPoints.Add(
                new CheckPoint {
                    RFID = ra.RFID,
                    AccId = ra.Runner.AccId,
                    RaceId = ra.RaceId,
                    ScannerId = ScannerId,
                    TimeStamp = DateTime.UtcNow
                }
            );
            _context.RaceAttendances.Update(ra);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("race/{RaceId}")]
        public async Task<ActionResult> GetAllCheckPointsFromRace(Guid RaceId)
        {
            var requester = await _context.Accounts.Select(a => new { a.AccId, a.Role }).FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));
            if (requester == null || requester.Role != "Admin") return Unauthorized();
            List<CheckPoint> l = _context.CheckPoints.Where(x => x.RaceId.Equals(RaceId)).ToList();
            return Ok(l);
        }
    }
}