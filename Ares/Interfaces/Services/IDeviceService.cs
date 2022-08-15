using API.DTOs.Vehicle;
using API.Helpers;

namespace API.Interfaces.Services
{
    public interface IDeviceService
    {
        Task<Response<bool>> AddVehicleToDriver(AddVehicleToDriverDto addVehicleToDriverDto, int appUserId);
    }
}
