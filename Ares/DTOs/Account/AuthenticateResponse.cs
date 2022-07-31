using API.Entities;

namespace API.DTOs.Account
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken {get;set;}
        public AuthenticateResponse(AppUser user, string jwtToken,string refreshToken)
        {
            Id = user.Id.ToString();
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.UserName;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}