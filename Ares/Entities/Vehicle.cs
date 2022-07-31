namespace API.Entities
{
    public class Vehicle : BaseEntity
    {
        public string PlateNumber { get; set; }
        public int EnterpriseId { get; set; }
        public Enterprise Enterprise { get; set; }
        public bool IsActive { get; set; }
        public IList<Location> Locations { get; set; }
        public IList<VehicleAppUser> VehicleAppUsers { get; set; }
    }
}
