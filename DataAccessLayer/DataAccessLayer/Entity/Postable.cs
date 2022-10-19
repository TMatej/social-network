using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccessLayer.Entity
{
    [Table("Postable")]
    public abstract class Postable : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
