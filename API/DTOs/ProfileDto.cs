namespace API.DTOs
{
    public class ProfileDto
    {
        public string Role { get; set; }
        public string KTP { get; set; }
        public string No_HP { get; set; }
        public DateOnly Birthday { get; set; }
    }
}