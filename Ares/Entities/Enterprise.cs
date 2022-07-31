namespace API.Entities
{
    public class Enterprise : BaseEntity
    {
        public string Name { get; set; }
        public int InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public IList<Vehicle> Vehicles { get; set; }
        public IList<AppUserEnterprise> AppUserEnterprises { get; set; }
    }
}
