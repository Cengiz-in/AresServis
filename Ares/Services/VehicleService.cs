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
        private readonly IRepository<Enterprise> _enterpriseRepository;
        private readonly IMapper _mapper;
        public VehicleService(IRepository<Vehicle> vehicleRepository, IRepository<Enterprise> enterpriseRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _enterpriseRepository = enterpriseRepository;
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


        public async Task<Response<bool>> UpdateVehicle(int vehicleId, AddVehicle model)
        {
            var vehicle = await _vehicleRepository.GetSingle(s => s.Id == vehicleId);
            vehicle.PlateNumber = model.PlateNumber;
            if (model.DeviceId != null) vehicle.DeviceId = model.DeviceId;
            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveAsync();
            return new Response<bool>(true);
        }

        public async Task<Response<PagedList<VehicleDto>>> GetVehicles(PaginationParams paginationParams, int userId)
        {

            var query = _vehicleRepository.GetAll(s => s.Enterprise.AppUserEnterprises.Any(q => q.AppUserId == userId), new[] { "VehicleAppUsers", "Device" }).ProjectTo<VehicleDto>(_mapper.ConfigurationProvider);
            var result = await PagedList<VehicleDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
            return new Response<PagedList<VehicleDto>>(result);
        }
    }
}
