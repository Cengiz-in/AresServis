using API.DTOs.Vehicle;
using API.Enums;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class VehicleController : BaseApiController
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [Authorize(Roles = $"{Roles.Driver}")]
        [HttpPut("{vehicleId}/status")]
        public async Task<ActionResult> SetVehicleStatus([FromBody]VehicleStatusDto model, int vehicleId)
        {
            return Ok(await _vehicleService.SetVehicleStatus(vehicleId,model.Status));
        }
    }
}
