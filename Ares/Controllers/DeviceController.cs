using API.DTOs.Vehicle;
using API.Enums;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DeviceController : BaseApiController
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        [Authorize(Roles = $"{Roles.Driver}")]
        [HttpPost]
        public async Task<ActionResult> AddVehicleToDriver([FromBody] AddVehicleToDriverDto model)
        {
            return Ok(await _deviceService.AddVehicleToDriver(model, GetLoggedInUserId()));
        }
    }
}
