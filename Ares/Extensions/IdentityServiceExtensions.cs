using System.Text;
using API.Data;
using API.Entities;
using API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt => {
                //// register and login validation
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            .AddEntityFrameworkStores<DataContext>().AddErrorDescriber<MultilanguageIdentityErrorDescriber>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                                ValidateIssuer = false,
                                ValidateAudience = false,
                                ClockSkew = System.TimeSpan.Zero,
                                ValidateLifetime = true
                            };
                        });
                        
            // Policy requires 
            // services.AddAuthorization(opt => {
            //     opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            //     opt.AddPolicy("ModerateAppointmentRole", policy => policy.RequireRole("Admin","Trainer"));
            // });
            
            return services;
        }
    }
}