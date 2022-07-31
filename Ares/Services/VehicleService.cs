using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Interfaces.Services;

namespace API.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        public VehicleService(IRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Response<bool>> SetVehicleStatus(int vehicleId, bool status)
        {
            var vehicle = await _vehicleRepository.GetSingle(s=> s.Id == vehicleId);
            vehicle.IsActive = status;
            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveAsync();
            return new Response<bool>(true); ;
        }
    }
}
