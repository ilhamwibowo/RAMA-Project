using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static API.Entities.RaceRegistration;

namespace API.Entities
{
    public class RaceHistoryDto
    {
        public int AccId { get; set; }
        public List<RaceRegistDto> Histories { get; set; } 

        public int Length {get; set;}

        
    }
}