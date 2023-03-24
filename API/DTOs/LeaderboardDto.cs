namespace API.DTOs
{
    public class LeaderboardDto
    {

        public string RaceName { get; set; }
        public List<RaceAttendanceDto> RaceAttendee { get; set; }
        public int TotalCount {get; set;}
        public int CurrentPage {get; set;}
        public int PageSize {get; set;}
        public int TotalPages {get; set;}
    }
}