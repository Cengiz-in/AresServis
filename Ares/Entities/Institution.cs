namespace API.Entities
{
    public class Institution : BaseEntity
    {
        public string Name { get; set; }
        public IList<Enterprise> Enterprises { get; set; }
        public IList<AppUser> AppUsers { get; set; }
    }
}
