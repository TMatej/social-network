using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class Conversation : IEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        public int UserId;

        [Required]
        public User User { get; set; }
    }
}
