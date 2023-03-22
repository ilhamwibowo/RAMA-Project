using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class RegistrationController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public RegistrationController(DataContext context, ITokenService tokenService, ILogger<AccountController> logger, IEmailService emailService, IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _logger = logger;
            _emailService = emailService;
            _mapper = mapper;
        }
        [HttpPost("{RaceId}")] 
        public async Task<ActionResult> CreateRaceRegistration(int RaceId)
        {
            Account requester = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null) return Unauthorized();

            Race race = await _context.Races.FirstOrDefaultAsync(r => r.RaceId == RaceId);

            if (race == null) return NotFound();

            if (await _context.RaceRegistrations.AnyAsync(rr => rr.AccId == requester.AccId && rr.RaceId == race.RaceId)) return BadRequest("Anda telah terdaftar!.");

            RaceRegistration newRR = new RaceRegistration 
                {
                    AccId = requester.AccId,
                    RaceId = RaceId,
                    RegistrationFee = race.RegistrationFee
                };
            requester.RaceHistory.Add(newRR);
            try 
                {
                    await _context.SaveChangesAsync();
                }
            catch (Exception e) 
                {
                    _logger.LogError(e, "Failed to save Race Registration");
                    return BadRequest("Failed to save Race Registration");
                }
            // email confirmation

            try
                {
                    await _emailService.SendEmailAsync(
                        new MailDto
                        {
                            ToEmail = requester.Email,
                            Subject = "Pendaftaran Event | RAMA",
                            Body = $"Haloo {requester.Name}! Selamat kamu telah berhasil mendaftarkan diri kamu ke Event {race.RaceName}. "+
                                   $"Namun, Registrasi Belum selesai, kamu masih perlu membayar Fee sebesar {race.RegistrationFee} rupiah untuk menyelesaikan pendaftaran."+
                                   $"Jangan Lupa yaa. Kami tunggu kehadiranmu di lapangan!!"
                        }
                    );
                }
            catch(Exception e)
                {
                    _logger.LogError(e, "Failed to send email");
                    return BadRequest("Failed to send email");
                }


            return CreatedAtAction(nameof(GetRaceRegist), new {RaceId}, _mapper.Map<RaceRegistDto>(newRR));
        }
        [HttpGet]
        public async Task<ActionResult<RaceHistoryDto>> GetHistory()
        {
            var requester = await _context.Accounts.Include(a => a.RaceHistory).Select(a => new { a.AccId, a.RaceHistory }).FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null) return Unauthorized();

            RaceHistoryDto rh = new RaceHistoryDto 
                {
                    AccId = requester.AccId,
                    Histories = _mapper.Map<IEnumerable<RaceRegistDto>>(requester.RaceHistory).ToList(),
                    Length = requester.RaceHistory.Count
                };

            return Ok(rh);
        }

        [HttpGet("all")]
        public async Task<ActionResult<RaceHistoryDto>> GetHistoryAll()
        {
            var requester = await _context.Accounts.Select(a => new { a.AccId, a.Role }).FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null || requester.Role != "Admin") return Unauthorized();

            List<RaceRegistration> AllRaceHistory = _context.RaceRegistrations.OrderBy(r => r.RegistedAt).ToList();

            RaceHistoryDto rh = new RaceHistoryDto 
                {
                    AccId = requester.AccId,
                    Histories = _mapper.Map<IEnumerable<RaceRegistDto>>(AllRaceHistory).ToList(),
                    Length = AllRaceHistory.Count
                };

            return Ok(rh);
        }

        [HttpGet("{RaceId}")]
        public async Task<ActionResult<RaceHistoryDto>> GetRaceRegist(int RaceId)
        {
            var requester = await _context.Accounts.Include(a => a.RaceHistory).Select(a => new { a.AccId, a.RaceHistory }).FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null) return Unauthorized();

            RaceRegistration rr = requester.RaceHistory.FirstOrDefault(r => r.RaceId == RaceId);

            if (rr == null) return NotFound();

            return Ok(_mapper.Map<RaceRegistDto>(rr));
        }

        

        [HttpPut("{RaceId}/{AccId}")]
        public async Task<ActionResult> UpdateRaceRegistration(int RaceId, int AccId, RaceRegistDto rrDto)
        {
            var requester = await _context.Accounts.Select(a => new { a.AccId, a.Role }).FirstOrDefaultAsync(x => x.AccId == User.GetUserId());


            if (requester == null || requester.Role != "Admin") return Unauthorized();

            RaceRegistration rr = await _context.RaceRegistrations.FirstOrDefaultAsync(x => x.RaceId == RaceId && x.AccId == AccId);

            if (rr == null) return NotFound();

            rr.Status = rrDto.Status;
            rr.RegistrationFee = rrDto.RegistrationFee;
            rr.PaidAt = rrDto.PaidAt;
            rr.TakenKitAt = rrDto.TakenKitAt;
            
            _context.RaceRegistrations.Update(rr);

            try 
                {
                    await _context.SaveChangesAsync();
                }
            catch (Exception e) 
                {
                    _logger.LogError(e, "Failed to update Race Registration");
                    return BadRequest("Failed to update Race Registration");
                }
            return Ok();
        }
        [HttpDelete("{RaceId}/{AccId}")]
        public async Task<ActionResult> DeleteRaceRegistration(int RaceId, int AccId)
        {
            var requester = await _context.Accounts.Select(a => new { a.AccId, a.Role }).FirstOrDefaultAsync(x => x.AccId == User.GetUserId());


            RaceRegistration rr = await _context.RaceRegistrations.FirstOrDefaultAsync(x => x.RaceId == RaceId && x.AccId == AccId);


            if (rr == null) return NotFound();

            // Registration can be deleted by Admin
            // and user can also delete their own Registration if its status is still 0.
            if (requester == null || (  requester.Role != "Admin" && (rr.Status != RaceRegistration.PaymentStatus.Waiting || requester.AccId != AccId) )) return Unauthorized();
            _logger.LogInformation($"{requester.Role != "Admin"}, {rr.Status != RaceRegistration.PaymentStatus.Waiting}, {requester.AccId != AccId}");





            
            _context.RaceRegistrations.Remove(rr);

            try 
                {
                    await _context.SaveChangesAsync();
                }
            catch (Exception e) 
                {
                    _logger.LogError(e, "Failed to delete Race Registration");
                    return BadRequest("Failed to delete Race Registration");
                }
            return Ok();
        }

    }
}