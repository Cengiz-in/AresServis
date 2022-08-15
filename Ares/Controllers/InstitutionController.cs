using API.DTOs.Pagination;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InstitutionController : BaseApiController
    {
        private readonly IInstitutionService _institutionService;

        public InstitutionController(IInstitutionService institutionService)
        {
            _institutionService = institutionService;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> DriverVehicles([FromQuery] PaginationParams pagingParams)
        {
            return Ok(await _institutionService.GetInstitutions(pagingParams, GetLoggedInUserId()));
        }
    }
}
