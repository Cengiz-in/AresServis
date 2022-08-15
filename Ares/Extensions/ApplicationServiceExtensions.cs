using API.Data;
using API.Services;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
using API.Interfaces.Services;
using API.Interfaces;
using API.Repository;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IEnterpriseService, EnterpriseService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IInstitutionService, InstitutionService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}