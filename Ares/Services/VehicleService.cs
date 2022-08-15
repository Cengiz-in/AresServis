using API.DTOs.Pagination;
using API.DTOs.Vehicle;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IMapper _mapper;
        public VehicleService(IRepository<Vehicle> vehicleRepository,IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> SetVehicleStatus(int vehicleId, bool status)
        {
            var vehicle = await _vehicleRepository.GetSingle(s=> s.Id == vehicleId);
            vehicle.IsActive = status;
            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveAsync();
            return new Response<bool>(true); ;
        }

        public async Task<Response<PagedList<VehicleDto>>> GetDriverVehicles(PaginationParams paginationParams,int appUserId)
        {
            var query = _vehicleRepository.GetAll(s => s.VehicleAppUsers.Any(q => q.AppUserId == appUserId), new[] { "VehicleAppUsers","Device" }).ProjectTo<VehicleDto>(_mapper.ConfigurationProvider);
            var tmp = query.ToList();
            var result = await PagedList<VehicleDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
            return new Response<PagedList<VehicleDto>>(result);
        }

        public async Task<Response<bool>> CreateVehicle(CreateVehicleDto createVehicleDto,int enterpriseId)
        {
            _vehicleRepository.Add(new Vehicle
            {
                EnterpriseId = enterpriseId,
                PlateNumber = createVehicleDto.PlateNumber,
            });
            await _vehicleRepository.SaveAsync();
            return new Response<bool>(true); ;
        }
    }
}
