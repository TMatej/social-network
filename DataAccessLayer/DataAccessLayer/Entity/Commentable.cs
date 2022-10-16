using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class Commentable : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
