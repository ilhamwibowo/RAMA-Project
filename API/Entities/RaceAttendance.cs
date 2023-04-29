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

        public List<CheckPoint> CheckPoints {get; set;} = new List<CheckPoint>();
    }
}