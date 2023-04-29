using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class CheckPoint
    {
        public Guid AccId {get; set;}
        public Guid RaceId {get; set;}
        public string ScannerId {get; set;}
        public DateTime TimeStamp {get; set;}
    }
}