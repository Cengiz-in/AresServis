namespace API.Entities
{
    public class Location : BaseEntity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int VehicleId { get; set; }
        public int AppuserId { get; set; }
        public AppUser AppUser { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
