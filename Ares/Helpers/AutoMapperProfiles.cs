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
                .ForMember(s => s.Device, opt => opt.MapFrom(q => q.Device))
                 .ForMember(s => s.PlateNumber, opt => opt.MapFrom(q => q.PlateNumber)); ;
            CreateMap<Enterprise, EnterpriseDto>();
            CreateMap<Institution, InstitutionDto>();
            CreateMap<VehicleAppUser, VehicleAppUserDto>()
               .ForMember(s => s.AppUserName, opt => opt.MapFrom(q => $"{q.AppUser.FirstName} {q.AppUser.LastName}"));
        }
    }
}