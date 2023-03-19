using API.Entities;

namespace API.DTOs
{
    public class RaceAttendanceDto
    {
        public string ProfilePhotoUrl { get; set; }
        public string Name { get; set; }
        public string BibNumber { get; set; }
        public int Position { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime FinishTime { get; set; }
    }
}