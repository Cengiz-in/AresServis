namespace API.Entities
{
    public class VehicleAppUser
    {
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
