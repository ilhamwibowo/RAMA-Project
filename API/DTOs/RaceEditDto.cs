using System.ComponentModel.DataAnnotations;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.DTOs
{
    public class RaceEditDto
    {
        [Required]
        public string RaceName { get; set; }
        [Required]
        public string RaceDesc {get; set;}
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public Location StartLocation { get; set; }
        [Required]
        public int Distance { get; set; }
        [Required]
        public int RegistrationFee { get; set; }
        public Guid AlbumId { get; set; }
        public List<Location> Points { get; set; }
        public List<RaceAttendanceDto> RaceAttendee { get; set; }
        public DateOnly StartDateRegistration { get; set; }
        public DateOnly EndDateRegistration { get; set; }
        public bool IsPublished { get; set; } = false;
        public bool IsOpened {get; set;}

        [FromForm(Name = "file")]
        public IFormFile photoFile {get; set;}
    }
}