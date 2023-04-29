using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CheckPointDto
    {
        [Required]
        public string RFID {get; set;}

        public Guid AccId {get; set;}
        [Required]
        public Guid RaceId {get; set;}

    }
}