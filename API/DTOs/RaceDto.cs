using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class RaceDto
    {
        public Guid RaceId {get; set;}
        [Required]
        public string RaceName { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public Location StartLocation { get; set; }
        [Required]
        public int Distance { get; set; }
        [Required]
        public int RegistrationFee { get; set; }
        public AlbumDto RaceAlbum { get; set; }
        public List<Location> Points { get; set; }
        public List<RaceAttendanceDto> RaceAttendee { get; set; } = new List<RaceAttendanceDto>();
    }
}