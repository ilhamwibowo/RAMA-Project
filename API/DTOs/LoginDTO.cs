using System.ComponentModel.DataAnnotations;
namespace API.DTOs
{
    public class LoginDto
    {
        [Required]
        [StringLength(maximumLength: 64, MinimumLength = 5)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}