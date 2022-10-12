using API.DTOs.Enterprise;
using API.DTOs.Instution;
using API.DTOs.Location;
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
                .ForMember(s => s.VehicleDriver, opt => opt.MapFrom(q => q.VehicleAppUsers.Any() ?
                    $"{q.VehicleAppUsers.FirstOrDefault().AppUser.FirstName} {q.VehicleAppUsers.FirstOrDefault().AppUser.LastName}" : ""));
            CreateMap<Enterprise, EnterpriseDto>();
            CreateMap<Institution, InstitutionDto>();
            CreateMap<VehicleAppUser, VehicleAppUserDto>()
               .ForMember(s => s.AppUserName, opt => opt.MapFrom(q => $"{q.AppUser.FirstName} {q.AppUser.LastName}"));
            CreateMap<Location, LocationHistoryDto>().ReverseMap();
        }
    }
}