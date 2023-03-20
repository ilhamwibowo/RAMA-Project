using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    public class RegistrationController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailService _emailService;

        public RegistrationController(DataContext context, ITokenService tokenService, ILogger<AccountController> logger, IEmailService emailService)
        {
            _context = context;
            _tokenService = tokenService;
            _logger = logger;
            _emailService = emailService;
        }
        [HttpPost("{RaceId}")] 
        public async Task<ActionResult> CreateRaceRegistration(int RaceId)
        {
            Account requester = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null) return Unauthorized();

            Race race = await _context.Races.FirstOrDefaultAsync(r => r.RaceId == RaceId);

            if (race == null) return NotFound();

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
                                   $"Namun, Registrasi Belum selesai, kamu masih perlu membayar Fee sebesar {race.RegistrationFee} rupiah untuk menyelesaikan pendaftaran. "+
                                   $"Jangan Lupa yaa. Kami tunggu kehadiranmu di lapangan!!"
                        }
                    );
                }
            catch(Exception e)
                {
                    _logger.LogError(e, "Failed to send email");
                    return BadRequest("Failed to send email");
                }


            return CreatedAtAction(nameof(GetRaceRegist), new {RaceId}, newRR);
        }
        [HttpGet]
        public async Task<ActionResult<RaceHistoryDto>> GetHistory()
        {
            Account requester = await _context.Accounts.Include(a => a.RaceHistory).FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null) return Unauthorized();

            List<RaceRegistDto> h = new List<RaceRegistDto>();

            foreach (RaceRegistration rr in requester.RaceHistory)
            {
                h.Add
                    (
                        new RaceRegistDto 
                        {
                            AccId = rr.AccId,
                            RaceId = rr.RaceId,
                            RegistedAt = rr.RegistedAt,
                            Status = rr.Status,
                            RegistrationFee = rr.RegistrationFee,
                            PaidAt = rr.PaidAt,
                            TakenKitAt = rr.TakenKitAt
                        }
                    );
            }

            RaceHistoryDto rh = new RaceHistoryDto 
                {
                    AccId = requester.AccId,
                    Histories = h,
                    Length = h.Count
                };

            return Ok(rh);
        }

        [HttpGet("all")]
        public async Task<ActionResult<RaceHistoryDto>> GetHistoryAll()
        {
            Account requester = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null && requester.Role != "Admin") return Unauthorized();

            List<RaceRegistration> AllRaceHistory = _context.RaceRegistrations.OrderBy(r => r.RegistedAt).ToList();

            List<RaceRegistDto> h = new List<RaceRegistDto>();

            foreach (RaceRegistration rr in AllRaceHistory)
            {
                h.Add
                    (
                        new RaceRegistDto 
                        {
                            AccId = rr.AccId,
                            RaceId = rr.RaceId,
                            RegistedAt = rr.RegistedAt,
                            Status = rr.Status,
                            RegistrationFee = rr.RegistrationFee,
                            PaidAt = rr.PaidAt,
                            TakenKitAt = rr.TakenKitAt
                        }
                    );
            }

            RaceHistoryDto rh = new RaceHistoryDto 
                {
                    AccId = requester.AccId,
                    Histories = h,
                    Length = h.Count
                };

            return Ok(rh);
        }

        [HttpGet("{RaceId}")]
        public async Task<ActionResult<RaceHistoryDto>> GetRaceRegist(int RaceId)
        {
            Account requester = await _context.Accounts.Include(a => a.RaceHistory).FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null) return Unauthorized();

            RaceRegistration rr = requester.RaceHistory.FirstOrDefault(r => r.RaceId == RaceId);

            if (rr == null) return NotFound();

            RaceRegistDto rrDto = new RaceRegistDto
                {
                    AccId = rr.AccId,
                    RaceId = rr.RaceId,
                    RegistedAt = rr.RegistedAt,
                    Status = rr.Status,
                    RegistrationFee = rr.RegistrationFee,
                    PaidAt = rr.PaidAt,
                    TakenKitAt = rr.TakenKitAt
                };
            return Ok(rr);
        }

        

        [HttpPut("{RaceId}/{AccId}")]
        public async Task<ActionResult> UpdateRaceRegistration(int RaceId, int AccId, RaceRegistDto rrDto)
        {
            Account requester = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null && requester.Role != "Admin") return Unauthorized();

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
            Account requester = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null && requester.Role != "Admin") return Unauthorized();

            RaceRegistration rr = await _context.RaceRegistrations.FirstOrDefaultAsync(x => x.RaceId == RaceId && x.AccId == AccId);

            if (rr == null) return NotFound();


            
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