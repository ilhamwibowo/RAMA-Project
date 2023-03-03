using API.Entities;

namespace API.DTOs
{
    public class RaceDto
    {
        public string RaceName { get; set; }

        public List<RaceAttendanceDto> RaceAttendee { get; set; } = new List<RaceAttendanceDto>();
    }
}