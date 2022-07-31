using API.Data;
using API.DTOs.Location;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Interfaces.Services;
using API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> _locationRepository;
        private readonly DataContext _context;
        public LocationService(IRepository<Location> locationRepository,DataContext context)
        {
            _locationRepository = locationRepository;
            _context = context;
        }
        public async Task<Response<bool>> AddLocation(LocationDto location, int vehicleId, int appUserId)
        {
            _locationRepository.Add(new Location
            {
                AppuserId = appUserId,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                VehicleId = vehicleId
            });
            await _locationRepository.SaveAsync();
            return new Response<bool>(true);
        }

        public async Task<Response<IList<VehicleLatestLocation>>> VehicleLocationsView()
        {
            var result = await _context.VehicleLatestLocations.ToListAsync();
            return new Response<IList<VehicleLatestLocation>>(result);
        }
    }
}
