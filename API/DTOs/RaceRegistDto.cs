using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static API.Entities.RaceRegistration;

namespace API.Entities
{
    public class RaceRegistDto
    {
        public int RaceId { get; set; }
        public int AccId { get; set; }
        public DateTime RegistedAt{ get; set; } = DateTime.UtcNow;

        public int RegistrationFee{ get; set; }

        // public Payment payment  // for later 
        public PaymentStatus Status { get; set;} = PaymentStatus.Waiting;

        public DateTime PaidAt{get; set;}
        public DateTime TakenKitAt{get; set;}
        
    }
}