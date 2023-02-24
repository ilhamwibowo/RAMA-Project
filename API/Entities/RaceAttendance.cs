using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class RaceAttendance
    {
        public int RaceId { get; set; }
        public int BibNumber { get; set; }

        public Account Runner { get; set; }


    }
}