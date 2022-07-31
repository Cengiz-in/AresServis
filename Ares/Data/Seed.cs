using System.Reflection;
using System.Text.Json;
using API.Entities;
using API.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;
            var kurum = new Institution
            {
                Name = "Ýzmir Büyükþehir Belediyesi"
            };
            context.Institutions.Add(kurum);
            await context.SaveChangesAsync();
            var admin = new AppUser
            {
                Email = "cihangir@cengiz.in",
                FirstName = "Cihangir",
                LastName = "Cengiz",
                PhoneNumber = "5071102474",
                UserName = "cihangircengiz",
                Guid = Guid.Parse("de94b650-a995-44a0-9d1f-e5641a475d68"),
                EmailConfirmed = true,
                NormalizedEmail = "CIHANGIR@CENGIZ.IN",
                NormalizedUserName = "CIHANGIRCENGIZ",
                Created = DateTime.Now,
                InstitutionId = kurum.Id
            };
            var surucu = new AppUser
            {
                Email = "fatihakbaba35@gmail.com",
                FirstName = "Fatih",
                LastName = "Akbaba",
                PhoneNumber = "5543697814",
                UserName = "fatihakbaba",
                Guid = Guid.Parse("33abd5ba-557c-49de-a70a-b14a17a64c91"),
                EmailConfirmed = true,
                NormalizedEmail = "FATIHAKBABA35@GMAIL.COM",
                NormalizedUserName = "FATIHAKBABA",
                Created = DateTime.Now,
                InstitutionId = kurum.Id
            };
            await userManager.CreateAsync(admin,"Password2");
            await userManager.AddToRoleAsync(admin, "Admin");
            await userManager.CreateAsync(surucu,"Password2");
            await userManager.AddToRoleAsync(surucu, "Driver");

            var kurulus = new Enterprise
            {
                Name = "Kýzýlay",
                InstitutionId = kurum.Id,
                Vehicles = new List<Vehicle>(),
                AppUserEnterprises = new List<AppUserEnterprise>()
            };

            var arac = new Vehicle
            {
                PlateNumber = "35DD3388",
                VehicleAppUsers = new List<VehicleAppUser>(),
                Locations = new List<Location>()
            };
            var aracSurucu = new VehicleAppUser
            {
                VehicleId = arac.Id,
                AppUserId = surucu.Id
            };

            kurulus.Vehicles.Add(arac);
            kurulus.AppUserEnterprises.Add(new AppUserEnterprise
            {
                EnterpriseId = kurulus.Id,
                AppUserId = admin.Id
            });
            kurulus.AppUserEnterprises.Add(new AppUserEnterprise
            {
                EnterpriseId = kurulus.Id,
                AppUserId = surucu.Id
            });
            arac.VehicleAppUsers.Add(aracSurucu);
            arac.Locations.Add(new Location
            {
                AppuserId = surucu.Id,
                Latitude = 38.346550,
                Longitude = 27.142309,
                VehicleId = arac.Id,
            });
            
            context.Enterprises.Add(kurulus);
            await context.SaveChangesAsync();

        }
        public static async Task SeedRoles(DataContext context, RoleManager<AppRole> roleManager)
        {
            if (await context.Users.AnyAsync()) return;
            var roles = new List<AppRole>();
            foreach (var role in typeof(Roles).GetMembers().Where(s => s.MemberType == System.Reflection.MemberTypes.Field))
            {
                roles.Add(new AppRole { Name = ((FieldInfo)role).GetValue(typeof(Roles)).ToString() });
            }
            await context.SaveChangesAsync().ConfigureAwait(false);
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}