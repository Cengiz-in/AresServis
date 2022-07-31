using API.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
    }
}
