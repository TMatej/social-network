using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity.JoinEntity
{
    public class UserRole : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
