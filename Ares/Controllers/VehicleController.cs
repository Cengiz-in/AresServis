using API.DTOs.Pagination;
using API.DTOs.Vehicle;
using API.Enums;
using API.Extensions;
using API.Interfaces.Services;
using API.Models.Exceptions;
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
        [HttpGet("{driverId}/vehicles")]
        public async Task<ActionResult> DriverVehicles([FromQuery] PaginationParams pagingParams,int driverId)
        {
            if (driverId != GetLoggedInUserId()) throw new CustomException("Sürücü bilgisi geçersiz");
            var result = await _vehicleService.GetDriverVehicles(pagingParams, GetLoggedInUserId());
            Response.AddPaginationHeader(result.Result.CurrentPage, result.Result.PageSize, result.Result.TotalCount, result.Result.TotalPages);
            return Ok(result);
        }

        [Authorize(Roles = $"{Roles.Driver}")]
        [HttpPut("{vehicleId}/status")]
        public async Task<ActionResult> SetVehicleStatus([FromBody]VehicleStatusDto model, int vehicleId)
        {
            return Ok(await _vehicleService.SetVehicleStatus(vehicleId,model.Status));
        }
    }
}
