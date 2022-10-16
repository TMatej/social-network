using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class Postable : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
