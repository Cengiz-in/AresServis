using API.DTOs.Vehicle;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Interfaces.Services;

namespace API.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IRepository<Device> _deviceRepository;
        private readonly IRepository<Vehicle> _vehicleRepository;

        public DeviceService(IRepository<Device> deviceRepository, IRepository<Vehicle> vehicleRepository)
        {
            _deviceRepository = deviceRepository;
            _vehicleRepository = vehicleRepository;
        }
        public async Task<Response<bool>> AddVehicleToDriver(AddVehicleToDriverDto addVehicleToDriverDto, int appUserId)
        {
            var device = await _deviceRepository.GetSingle(s => s.Name == addVehicleToDriverDto.DeviceName);
            var vehicle = await _vehicleRepository.GetSingle(s => s.DeviceId == device.Id);
            vehicle.VehicleAppUsers.Add(new VehicleAppUser
            {
                AppUserId = appUserId,
            });
            await _vehicleRepository.SaveAsync();
            return new Response<bool>(true); ;
        }
    }
}
