using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Entity.Enum;

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

        public GroupRole GroupRole { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
