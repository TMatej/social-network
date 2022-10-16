using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    [Table("Commentable")]
    public abstract class Commentable
    {
        [Key]
        public int Id { get; set; }
    }
}
