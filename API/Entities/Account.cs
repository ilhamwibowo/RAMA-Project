using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Entities
{
    public class Account
    {
        [Key]
        public int AccId { get; set; }
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string KTP { get; set; }

        public string No_HP { get; set; }

        public DateOnly Birthday { get; set; }

        public Photo ProfilePhoto { get; set; }

        public List<RaceRegistration> RaceHistory { get; set; }= new List<RaceRegistration>();

    }
}