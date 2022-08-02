using API.DTOs.Vehicle;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Vehicle, VehicleDto>();
        }
    }
}