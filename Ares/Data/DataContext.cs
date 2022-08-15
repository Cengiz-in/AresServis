using API.Entities;
using API.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<VehicleAppUser>()
                .HasKey(va => new { va.AppUserId, va.VehicleId });

            builder.Entity<VehicleAppUser>()
                .HasOne(a => a.AppUser)
                .WithMany(va=> va.VehicleAppUsers)
                .HasForeignKey(va => va.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VehicleAppUser>()
                .HasOne(a => a.Vehicle)
                .WithMany(va => va.VehicleAppUsers)
                .HasForeignKey(va => va.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppUserEnterprise>()
                .HasKey(va => new { va.AppUserId, va.EnterpriseId });

            builder.Entity<AppUserEnterprise>()
                .HasOne(a => a.AppUser)
                .WithMany(va => va.AppUserEnterprises)
                .HasForeignKey(va => va.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppUserEnterprise>()
                .HasOne(a => a.Enterprise)
                .WithMany(va => va.AppUserEnterprises)
                .HasForeignKey(va => va.EnterpriseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Location>()
                .HasOne(a => a.Vehicle)
                .WithMany(v => v.Locations)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VehicleLatestLocation>()
                .ToView(nameof(VehicleLatestLocations))
                .HasKey(t => t.Id);

        }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleAppUser> VehicleAppUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<AppUserEnterprise> AppUserEnterprises { get; set; }
        public DbSet<VehicleLatestLocation> VehicleLatestLocations { get; set; }
        public DbSet<Device> Devices { get; set; }
    }
}
