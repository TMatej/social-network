using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Entity.JoinEntity;

namespace DataAccessLayer.Entity
{
    public class Group : Postable
    {
        [Required]
        [MinLength(4)]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public IList<GroupMember> GroupMembers { get; set; }
    }
}
