using System.ComponentModel.DataAnnotations;
using Google.Api.Gax.ResourceNames;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Entities
{
    public class Race
    {
        [Key]
        public int RaceId { get; set; }

        public string RaceName { get; set; }

        public DateTime StartTime { get; set; }

        public Location StartLocation { get; set; }

        public int Distance { get; set; }

        public int RegistrationFee { get; set; }

        public List<Location> Points { get; set; } = new List<Location>();

        public List<RaceAttendance> RaceAttendee { get; set; } = new List<RaceAttendance>();

        public Album RaceAlbum { get; set; }
    }
}