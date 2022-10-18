using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public abstract class Commentable : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
