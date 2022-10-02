using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    [Table("GroupMember")]
    public class GroupMember
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int GroupRoleId { get; set; }
        public GroupRole GroupRole{ get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
