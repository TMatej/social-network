using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    [Table("Commentable")]
    public abstract class Commentable : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
