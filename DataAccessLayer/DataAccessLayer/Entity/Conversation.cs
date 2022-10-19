using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class Conversation : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId;

        [Required]
        public User User { get; set; }
    }
}
