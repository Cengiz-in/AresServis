using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimPrimcipleExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user){
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}