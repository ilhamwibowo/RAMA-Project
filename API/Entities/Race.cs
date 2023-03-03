using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Entities
{
    public class Race
    {
        [Key]
        public int RaceId { get; set; }

        public string RaceName { get; set; }

        public List<RaceAttendance> RaceAttendee { get; set; } = new List<RaceAttendance>();

        public Album RaceAlbum { get; set; }
    }
}