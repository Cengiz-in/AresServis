using API.DTOs.Account;
using API.Helpers;

namespace API.Interfaces.Services
{
    public interface IUserService
    {
        Task<Response<AuthenticateResponse>> Login(AuthenticateRequest model, string ipAddress);
        Task<Response<AuthenticateResponse>> RefreshToken(string token, string ipAddress);
        Task<Response<bool>> RevokeToken(string token, string ipAddress);
        Task<Response<bool>> RegisterDriver(RegisterDriverDto model, string ipAdress);
    }
}