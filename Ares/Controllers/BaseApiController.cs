using API.Entities;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected int GetLoggedInUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        protected Guid GetLoggedInUserGuid()
        {
            return new Guid(User.FindFirst(ClaimTypes.Name)?.Value);
        }
    }
}