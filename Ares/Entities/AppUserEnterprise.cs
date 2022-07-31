namespace API.Entities
{
    public class AppUserEnterprise
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int EnterpriseId { get; set; }
        public Enterprise Enterprise { get; set; }
    }
}
