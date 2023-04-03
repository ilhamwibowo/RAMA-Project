using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Google.Api.Gax.ResourceNames;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Entities
{
    public class Race
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RaceId { get; set; }

        public string RaceName { get; set; }
        public string RaceDesc { get; set; }
        public Photo RaceThumbnail { get; set; }

        public DateTime StartTime { get; set; }

        public Location StartLocation { get; set; }

        public int Distance { get; set; }

        public int RegistrationFee { get; set; }

        public List<Location> Points { get; set; } = new List<Location>();

        public List<RaceAttendance> RaceAttendee { get; set; } = new List<RaceAttendance>();

        public Album RaceAlbum { get; set; }
        public DateOnly StartDateRegistration { get; set; }
        public DateOnly EndDateRegistration { get; set; }
        public bool IsPublished { get; set; }
        public bool IsOpened()
        {
            return StartDateRegistration < DateOnly.FromDateTime(DateTime.UtcNow) && DateOnly.FromDateTime(DateTime.UtcNow) < EndDateRegistration;
        }
    }
}