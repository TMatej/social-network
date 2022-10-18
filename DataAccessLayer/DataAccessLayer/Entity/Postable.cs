using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public abstract class Postable : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
