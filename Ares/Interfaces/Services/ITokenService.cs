using API.Entities;

namespace API.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);

        RefreshToken CreateRefreshToken(int appUserId, string ipAddress);
    }
}