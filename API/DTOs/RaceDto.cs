using System.ComponentModel.DataAnnotations;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.DTOs
{
    public class RaceDto
    {
        public Guid RaceId {get; set;}
        public string RaceName { get; set; }
        public string RaceDesc {get; set;}
        public DateTime StartTime { get; set; }
        public Location StartLocation { get; set; }
        public int Distance { get; set; }
        public int RegistrationFee { get; set; }
        public AlbumDto RaceAlbum { get; set; }
        public List<Location> Points { get; set; }
        public List<RaceAttendanceDto> RaceAttendee { get; set; }
        public DateOnly StartDateRegistration { get; set; }
        public DateOnly EndDateRegistration { get; set; }
        public bool IsPublished { get; set; }
        public bool IsOpened {get; set;}
        public PhotoDto RaceThumbnail { get; set; }
    }
}