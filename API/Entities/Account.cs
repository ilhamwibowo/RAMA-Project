using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Entities
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AccId { get; set; }
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

        public string GetPhotoUrl(){
            return ProfilePhoto?.Url;
        }

    }
}