using API.Entities;

namespace API.DTOs.Vehicle
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public bool IsActive { get; set; }
        public Device Device { get; set; }
        public string VehicleDriver { get; set; }
    }
}
