using API.Entities;

namespace API.DTOs
{
    public class RaceDto
    {
        public int RaceId { get; set; }

        public string RaceName { get; set; }

        public List<RaceAttendance> RaceAttendee { get; set; } = new List<RaceAttendance>();

        public Album RaceAlbum { get; set; }
    }
}