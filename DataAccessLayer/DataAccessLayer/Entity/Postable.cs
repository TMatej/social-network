using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    [Table("Postable")]
    public abstract class Postable : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
