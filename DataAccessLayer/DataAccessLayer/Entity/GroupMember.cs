using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class GroupMember : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GroupId { get; set; }
        
        [Required]
        public Group Group { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int GroupRoleId { get; set; }
        
        [Required]
        public GroupRole GroupRole{ get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
