namespace API.DTOs
{
    public class ProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Role { get; set; }
        public string KTP { get; set; }
        public string No_HP { get; set; }
        public DateOnly Birthday { get; set; }

        public PhotoDto ProfilePhoto { get; set; }
    }
}