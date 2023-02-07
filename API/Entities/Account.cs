using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Account
    {
        [Key]
        public int AccId { get; set; }

        public string? Email { get; set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public string? Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? KTP { get; set; }

        public string? No_HP { get; set; }

        public DateOnly Birthday { get; set; }
    }
}