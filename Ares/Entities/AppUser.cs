using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public Guid Guid { get; set; }
        public int InstitutionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime Created { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime LastActive { get; set; } = DateTime.Now;
        public Institution Institution { get; set; }
        public IList<AppUserRole> UserRoles { get; set; }
        [JsonIgnore]
        public IList<RefreshToken> RefreshTokens { get; set; }

        public IList<VehicleAppUser> VehicleAppUsers { get; set; }
        public IList<AppUserEnterprise> AppUserEnterprises { get; set; }
    }
}
