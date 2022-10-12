using API.DTOs.Location;
using API.Enums;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LocationController : BaseApiController
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [Authorize(Roles = $"{Roles.Driver}")]
        [HttpPost("{vehicleId}")]
        public async Task<ActionResult> VehicleLocation([FromBody] LocationDto locationDto,int vehicleId)
        {
            return Ok(await _locationService.AddLocation(locationDto,vehicleId,GetLoggedInUserId()));
        }

        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpGet]
        public async Task<ActionResult> VehicleLocations()
        {
            return Ok(await _locationService.VehicleLocationsView());
        }

        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpGet("{vehicleId}")]
        public async Task<ActionResult> VehicleLocations(int vehicleId, DateTime startDate, DateTime endDate)
        {
            return Ok(await _locationService.VehicleLocationHistory(vehicleId,startDate,endDate));
        }
    }
}
