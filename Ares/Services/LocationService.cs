using API.Data;
using API.DTOs.Location;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Interfaces.Services;
using API.Models.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> _locationRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LocationService(IRepository<Location> locationRepository,DataContext context, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _context = context;
            _mapper = mapper;
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

        public async Task<Response<IList<LocationHistoryDto>>> VehicleLocationHistory(int vehicleId, DateTime startDate, DateTime endDate)
        {
            var request = await _context.Locations.Where(s => s.VehicleId == vehicleId && s.CreateDate >= startDate && s.CreateDate <= endDate).OrderBy(s=> s.CreateDate).ToListAsync();
            var result = _mapper.Map<List<LocationHistoryDto>>(request);
            return new Response<IList<LocationHistoryDto>>(result);
        }

        public async Task<Response<IList<VehicleLatestLocation>>> VehicleLocationsView()
        {
            var result = await _context.VehicleLatestLocations.ToListAsync();
            return new Response<IList<VehicleLatestLocation>>(result);
        }
    }
}
