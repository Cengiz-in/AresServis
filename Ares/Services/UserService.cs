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

        public async Task<Response<bool>> RegisterDriver(RegisterDriverDto model, string ipAdress)
        {
            if (await UserExists(model.Email,model.PhoneNumber)) throw new CustomException("Kullanıcı zaten kayıtlı");
            var vehicle = await VehicleExists(model.PlateNumber);
            if (vehicle == null) throw new CustomException("Araç sistemde kayıtlı değil");
            var user = new AppUser()
            {
                Guid = Guid.NewGuid(),
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = $"{model.FirstName.Trim().Replace(" ", "")}{model.LastName.Trim().Replace(" ", "")}".GenerateUsername(),
                PhoneNumber = model.PhoneNumber,
                InstitutionId = vehicle.Enterprise.InstitutionId
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Driver);
                _context.AppUserEnterprises.Add(new AppUserEnterprise
                {
                    AppUserId = user.Id,
                    EnterpriseId = vehicle.EnterpriseId
                });
                _context.VehicleAppUsers.Add(new VehicleAppUser
                {
                    AppUserId = user.Id,
                    VehicleId = vehicle.Id,
                });
                await _context.SaveChangesAsync();
                return new Response<bool>(true);
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

        private async Task<Vehicle> VehicleExists(string plateNumber)
        {
            var result = await _context.Vehicles.Include(s => s.Enterprise).FirstOrDefaultAsync(s => s.PlateNumber == plateNumber).ConfigureAwait(false);
            return result;
        }
        private async Task<bool> UserExists(string email,string phoneNumber)
        {
            return await _userManager.Users.AnyAsync(s => s.Email.ToLower() == email.ToLower() || s.PhoneNumber.Contains(phoneNumber));
        }

        public async Task<Response<bool>> Register(RegisterDto model, string ipAdress)
        {
            var enterpriseId = 2;
            var institutionId = 2;

            if (await UserExists(model.Email,model.PhoneNumber)) throw new CustomException("Kullanıcı zaten kayıtlı");
            var vehicle = new Vehicle
            {
                EnterpriseId = enterpriseId,
                PlateNumber = model.PlateNumber,
            };
            
            _context.Vehicles.Add(vehicle);
            var user = new AppUser()
            {
                Guid = Guid.NewGuid(),
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = $"{model.FirstName.Trim().Replace(" ", "")}{model.LastName.Trim().Replace(" ", "")}".GenerateUsername(),
                PhoneNumber = model.PhoneNumber,
                InstitutionId = institutionId
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Driver);
                _context.AppUserEnterprises.Add(new AppUserEnterprise
                {
                    AppUserId = user.Id,
                    EnterpriseId = enterpriseId
                });
                _context.VehicleAppUsers.Add(new VehicleAppUser
                {
                    AppUserId = user.Id,
                    VehicleId = vehicle.Id,
                });
                await _context.SaveChangesAsync();
                return new Response<bool>(true);
            }

            throw new IdentityException(result.Errors);
        }

        public async Task<Response<bool>> Delete(int userId)
        {
            var appuserenterprise = await _context.AppUserEnterprises.FirstAsync(s => s.AppUserId == userId);
            var appuserVehicles = _context.VehicleAppUsers.Include(s=> s.Vehicle).Where(s => s.AppUserId == userId);
            var user = await _userManager.Users.FirstAsync(s => s.Id == userId);
            var vehicles = appuserVehicles.Select(s => s.Vehicle);
            _context.AppUserEnterprises.Remove(appuserenterprise);
            _context.VehicleAppUsers.RemoveRange(appuserVehicles);
            _context.Vehicles.RemoveRange(vehicles);
            _context.Users.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception er )
            {

                throw;
            }
            return new Response<bool>(true);
        }
    }
}