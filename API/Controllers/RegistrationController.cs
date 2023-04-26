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
        public async Task<ActionResult> CreateRaceRegistration(Guid RaceId)
        {
            Account requester = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));

            if (requester == null) return Unauthorized();

            Race race = await _context.Races.FirstOrDefaultAsync(r => r.RaceId.Equals(RaceId));

            if (race == null) return NotFound();

            if (await _context.RaceRegistrations.AnyAsync(rr => rr.AccId == requester.AccId && rr.RaceId == race.RaceId)) return BadRequest("Anda telah terdaftar!.");

            RaceRegistration newRR = new RaceRegistration 
                {
                    AccId = requester.AccId,
                    RaceId = RaceId,
                    RegistrationFee = race.RegistrationFee
                };
            requester.RaceHistory.Add(newRR);
            await _context.SaveChangesAsync();
            // email confirmation
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


            return CreatedAtAction(nameof(GetRaceRegist), new {RaceId}, _mapper.Map<RaceRegistDto>(newRR));
        }
        [HttpGet]
        public async Task<ActionResult<RaceHistoryDto>> GetHistory()
        {
            var requester = await _context.Accounts.Include(a => a.RaceHistory).Select(a => new { a.AccId, a.RaceHistory }).FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));

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
        public async Task<ActionResult<RaceHistoryDto>> GetHistoryAll(string query = null)
        {
            if (query == null) query = string.Empty;
            var requester = await _context.Accounts.Select(a => new { a.AccId, a.Role }).FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));
            if (requester == null || requester.Role != "Admin") return Unauthorized();

            List<RaceRegistration> AllRaceHistory = _context.RaceRegistrations.OrderBy(r => r.RegistedAt).ToList();
            if (query != string.Empty) 
            {
                var accounts = _context.Accounts.Where(a => a.Name.ToLower().Contains(query.ToLower())).ToList().Select(a => new {a.AccId, a.Name});
                AllRaceHistory = AllRaceHistory.FindAll( r => accounts.Any((a) => r.AccId.Equals(a.AccId))).ToList();
            }
            RaceHistoryDto rh = new RaceHistoryDto 
                {
                    AccId = requester.AccId,
                    Histories = _mapper.Map<IEnumerable<RaceRegistDto>>(AllRaceHistory).ToList(),
                    Length = AllRaceHistory.Count
                };

            return Ok(rh);
        }

        [HttpGet("all/{RaceId}")]
        public async Task<ActionResult<RaceHistoryDto>> GetHistoryAllonRace(Guid RaceId, string query = null)
        {
            if (query == null) query = string.Empty;

            var requester = await _context.Accounts.Select(a => new { a.AccId, a.Role }).FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));

            if (requester == null || requester.Role != "Admin") return Unauthorized();

            List<RaceRegistration> AllRaceHistory = _context.RaceRegistrations.Where(r => r.RaceId.Equals(RaceId)).OrderBy(r => r.RegistedAt).ToList();

            if (query != string.Empty) 
            {
                var accounts = _context.Accounts.Where(a => a.Name.ToLower().Contains(query.ToLower())).ToList().Select(a => new {a.AccId, a.Name});
                AllRaceHistory = AllRaceHistory.FindAll( r => accounts.Any((a) => r.AccId.Equals(a.AccId))).ToList();
            }
            RaceHistoryDto rh = new RaceHistoryDto 
                {
                    AccId = requester.AccId,
                    Histories = _mapper.Map<IEnumerable<RaceRegistDto>>(AllRaceHistory).ToList(),
                    Length = AllRaceHistory.Count
                };

            return Ok(rh);
        }

        [HttpGet("{RaceId}")]
        public async Task<ActionResult<RaceHistoryDto>> GetRaceRegist(Guid RaceId)
        {
            var requester = await _context.Accounts.Include(a => a.RaceHistory).Select(a => new { a.AccId, a.RaceHistory }).FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));

            if (requester == null) return Unauthorized();

            RaceRegistration rr = requester.RaceHistory.FirstOrDefault(r => r.RaceId.Equals(RaceId));

            if (rr == null) return NotFound();

            return Ok(_mapper.Map<RaceRegistDto>(rr));
        }

        

        [HttpPut("{RaceId}/{AccId}")]
        public async Task<ActionResult> UpdateRaceRegistration(Guid RaceId, Guid AccId, RaceRegistDto rrDto)
        {
            var requester = await _context.Accounts.FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));


            if (requester == null || requester.Role != "Admin") return Unauthorized();

            RaceRegistration rr = await _context.RaceRegistrations.FirstOrDefaultAsync(x => x.RaceId.Equals(RaceId) && x.AccId.Equals(AccId));

            if (rr == null) return NotFound();

            rr.Status = rrDto.Status;
            if (rr.Status.Equals(RaceRegistration.PaymentStatus.Paid)) rr.PaidAt = DateTime.UtcNow;
            if (rr.Status.Equals(RaceRegistration.PaymentStatus.TakenKit)) rr.TakenKitAt = DateTime.UtcNow;
            if (rrDto.RFID != null) {
                rr.RFID = rrDto.RFID;
                RaceAttendance ra = new RaceAttendance 
                {
                    RaceId = RaceId,
                    Runner = requester,
                    BibNumber = await GenerateBibNumber("", 5, RaceId),
                };
                ra.RFID = rrDto.RFID;
                _context.RaceAttendances.Update(ra);
            }
            rr.RegistrationFee = rrDto.RegistrationFee;
            
            _context.RaceRegistrations.Update(rr);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{RaceId}/{AccId}")]
        public async Task<ActionResult> DeleteRaceRegistration(Guid RaceId, Guid AccId)
        {
            var requester = await _context.Accounts.Select(a => new { a.AccId, a.Role }).FirstOrDefaultAsync(x => x.AccId.Equals(User.GetUserId()));


            RaceRegistration rr = await _context.RaceRegistrations.FirstOrDefaultAsync(x => x.RaceId.Equals(RaceId) && x.AccId.Equals(AccId));


            if (rr == null) return NotFound();

            // Registration can be deleted by Admin
            // and user can also delete their own Registration if its status is still 0.
            if (requester == null || (  requester.Role != "Admin" && (rr.Status != RaceRegistration.PaymentStatus.Waiting || !requester.AccId.Equals(AccId)) )) return Unauthorized();
            
            _context.RaceRegistrations.Remove(rr);
            await _context.SaveChangesAsync();
            return Ok();
        }
        private async Task<string> GenerateBibNumber(string prefix, int len, Guid RaceId)
        {
            Random rnd = new Random();
            string BibNumber;
            do {
                int rint = rnd.Next(0, (int) Math.Pow(10, len) - 1);
                BibNumber = prefix + rint.ToString();
            } while (await CheckBibTagExist(BibNumber, RaceId));
            return BibNumber;
        }

        private async Task<bool> CheckBibTagExist(string bib, Guid RaceId)
        {
            var check = await _context.Races.Select(x => new {x.RaceId, x.RaceAttendee}).FirstOrDefaultAsync(x => x.RaceAttendee.Any(ra => ra.BibNumber == bib));
            return check == null;
        }
    }
}