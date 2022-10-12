using API.DTOs.Account;
using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces.Services;
using API.Models.Exceptions;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(AuthenticateRequest model)
        {
            return Ok(await _userService.Login(model, Request.ipAddress(HttpContext)));
        }

        [AllowAnonymous]
        [HttpPut("refreshToken")]
        public async Task<ActionResult> RefreshToken([FromBody] RevokeTokenRequest model)
        {
            var refreshToken = model.Token;
            return Ok(await _userService.RefreshToken(refreshToken, Request.ipAddress(HttpContext)));
        }

        [AllowAnonymous]
        [HttpDelete("revokeToken")]
        public async Task<ActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                throw new CustomException("Token is required");
            return Ok(await _userService.RevokeToken(token, Request.ipAddress(HttpContext)));
        }

        [HttpPost("register-driver")]
        public async Task<ActionResult> RegisterDriverAccount(RegisterDriverDto model)
        {
            return Ok(await _userService.RegisterDriver(model, Request.ipAddress(HttpContext)));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            return Ok(await _userService.Register(model, Request.ipAddress(HttpContext)));
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> RemoveUser()
        {
            var userId = GetLoggedInUserId();
            return Ok(await _userService.Delete(userId));
        }
    }
}