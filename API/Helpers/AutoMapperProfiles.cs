using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Race, RaceDto>();
            CreateMap<RaceRegistration, RaceRegistDto>();
            CreateMap<RaceAttendance, RaceAttendanceDto>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<Account, ProfileDto>();
            CreateMap<Album, AlbumDto>();

        }
    }
}