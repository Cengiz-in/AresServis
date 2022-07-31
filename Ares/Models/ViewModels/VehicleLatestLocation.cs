namespace API.Models.ViewModels
{
    public class VehicleLatestLocation
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int VehicleId { get; set; }
        public int AppuserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public long Recency { get; set; }
    }
}
