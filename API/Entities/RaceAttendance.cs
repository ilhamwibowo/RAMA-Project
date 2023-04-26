using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class RaceAttendance
    {
        public Guid RaceId { get; set; }
        public string BibNumber { get; set; }

        public Account Runner { get; set; }

        public int Position { get; set;}

        public TimeSpan Duration {get; set;}

        public DateTime FinishTime{get; set;}
        public string RFID {get; set;}

        public Dictionary<Location, DateTime> CheckPoints = new Dictionary<Location, DateTime>();
    }
}