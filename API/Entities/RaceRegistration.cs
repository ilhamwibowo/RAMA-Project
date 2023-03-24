using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class RaceRegistration
    {
        public enum PaymentStatus
        {
            Waiting,
            Paid,
            TakenKit, //taken the Runner Kit, Changed when user take runner kit.
            Completed //Event is Over. Changed by admin?
        };
        public Guid RaceId { get; set; }
        public Guid AccId { get; set; }
        public DateTime RegistedAt{ get; set; } = DateTime.UtcNow;

        public int RegistrationFee{ get; set; }

        // public Payment payment  // for later 
        public PaymentStatus Status { get; set;} = PaymentStatus.Waiting;

        public DateTime PaidAt{get; set;}
        public DateTime TakenKitAt{get; set;}
        
        public string GetStatusAsText()
        {
            return Status.ToString();
        }
    }
}