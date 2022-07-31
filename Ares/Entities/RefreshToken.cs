using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class RefreshToken : BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Token { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpireDate { get; set; }
        public string CreatedByIp { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RevokedDate { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }
        
    }
}