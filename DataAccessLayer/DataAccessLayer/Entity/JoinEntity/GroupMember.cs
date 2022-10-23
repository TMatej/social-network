using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity.JoinEntity
{
    public class GroupMember : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int GroupRoleId { get; set; }

        [ForeignKey(nameof(GroupRoleId))]
        public GroupRole GroupRole { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
