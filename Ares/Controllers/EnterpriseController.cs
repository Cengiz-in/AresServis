using API.DTOs.Pagination;
using API.Enums;
using API.Interfaces.Services;
using API.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EnterpriseController : BaseApiController
    {
        private readonly IEnterpriseService _enterpriseService;

        public EnterpriseController(IEnterpriseService enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> DriverVehicles([FromQuery] PaginationParams pagingParams)
        {
            return Ok(await _enterpriseService.GetEnterprises(pagingParams,GetLoggedInUserId()));
        }
    }
}
