using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Entities;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public RefreshToken CreateRefreshToken(int appUserId,string ipAddress)
        {
            
            using(var rngCryptoServiceProvider = RandomNumberGenerator.Create())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    ExpireDate = DateTime.UtcNow.AddDays(14),
                    CreateDate = DateTime.UtcNow,
                    CreatedByIp = ipAddress,
                    AppUserId = appUserId,
                };
            }
        }

        public async Task<string> CreateToken(AppUser user)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Guid.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            
            // single role
            ///claims.Add(roles.Select(role => new Claim(ClaimTypes.Role, role)).FirstOrDefault());
            // multiple role
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role,role)));
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        
    }
}