using API.DTOs.Account;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
using API.Extensions;
using API.Enums;
using API.Data;
using API.Interfaces.Services;
using API.Models.Exceptions;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DataContext _context;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, DataContext context)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<Response<AuthenticateResponse>> Login(AuthenticateRequest model, string ipAddress)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(s => s.Email == model.Login.ToLower() || s.PhoneNumber == model.Login.ToLower() || s.UserName == model.Login);
            if (user == null) throw new CustomException("Geçersiz e-posta, telefon numarası veya şifre");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) throw new CustomException("Geçersiz e-posta, telefon numarası veya şifre");

            var role = await _userManager.GetRolesAsync(user);
            var refreshToken = _tokenService.CreateRefreshToken(user.Id,ipAddress);
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            await _userManager.UpdateAsync(user);
            return new Response<AuthenticateResponse>(new AuthenticateResponse(user, await _tokenService.CreateToken(user), refreshToken.Token));

        }

        public async Task<Response<AuthenticateResponse>> RefreshToken(string token, string ipAddress)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(s => s.Token == token);
            
            if (refreshToken == null) throw new CustomException("Geçersiz token");


            if (refreshToken.RevokedDate.HasValue || DateTime.Now >= refreshToken.ExpireDate) throw new CustomException("Token süresi doldu");

            var newRefreshToken = _tokenService.CreateRefreshToken(refreshToken.AppUserId,ipAddress);
            refreshToken.RevokedDate = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();
            var user = await _context.Users.FirstAsync(s => s.Id == refreshToken.AppUserId);

            var jwtToken = await _tokenService.CreateToken(user);

            return new Response<AuthenticateResponse>(new AuthenticateResponse(user, jwtToken, newRefreshToken.Token));
        }

        public async Task<Response<AuthenticateResponse>> RegisterAccount(RegisterAccountDto model, string ipAdress)
        {
            var role = typeof(Roles).GetProperties().First(s => s.Name == model.Role).Name;
            var user = new AppUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = $"{model.FirstName.Trim().Replace(" ", "")}{model.LastName.Trim().Replace(" ", "")}".GenerateUsername(),
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
                return await Login(new AuthenticateRequest
                {
                    Login = model.Email,
                    Password = model.Password
                }, ipAdress);
            }

            throw new IdentityException(result.Errors);
        }

        public async Task<Response<bool>> RevokeToken(string token, string ipAddress)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(s => s.Token == token);
            // return false if no user found with token
            if (refreshToken == null) throw new CustomException("Geçersiz token");

            // return false if token is not active
            if (refreshToken.RevokedDate.HasValue || DateTime.Now >= refreshToken.ExpireDate) throw new CustomException("Token süresi doldu");

            // revoke token and save
            refreshToken.RevokedDate = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            return new Response<bool>(await _context.SaveChangesAsync() > 0);
        }
    }
}