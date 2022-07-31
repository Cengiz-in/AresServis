using API.Helpers;

namespace API.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<Response<bool>> SetVehicleStatus(int vehicleId,bool status);
    }
}
