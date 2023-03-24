using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static API.Entities.RaceRegistration;

namespace API.Entities
{
    public class RaceRegistDto
    {
        public Guid RaceId { get; set; }
        public Guid AccId { get; set; }
        public DateTime RegistedAt{ get; set; } = DateTime.UtcNow;

        public int RegistrationFee{ get; set; }

        // public Payment payment  // for later 
        public PaymentStatus Status { get; set;}
        public string StatusAsText {get; set; }

        public DateTime PaidAt{get; set;}
        public DateTime TakenKitAt{get; set;}
        
    }
}