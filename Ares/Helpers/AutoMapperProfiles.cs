using API.DTOs.Enterprise;
using API.DTOs.Instution;
using API.DTOs.Vehicle;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(s => s.Device, opt => opt.MapFrom(q => q.Device));
            CreateMap<Enterprise, EnterpriseDto>();
            CreateMap<Institution, InstitutionDto>();
        }
    }
}