using API.DTOs.Location;
using API.Helpers;
using API.Models.ViewModels;

namespace API.Interfaces.Services
{
    public interface ILocationService
    {
        Task<Response<bool>> AddLocation(LocationDto location, int vehicleId, int appUserId);
        Task<Response<IList<VehicleLatestLocation>>> VehicleLocationsView();
    }
}
