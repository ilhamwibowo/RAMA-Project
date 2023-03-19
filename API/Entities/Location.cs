using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace API.Entities
{
    public class Location
    {
        [Key]
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}