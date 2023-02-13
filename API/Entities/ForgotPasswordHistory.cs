using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ForgotPasswordHistory
    {
        public string Email { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Otp { get; set; }
        public int RequestCount { get; set; }
        public int CheckCount { get; set;}


    }
}