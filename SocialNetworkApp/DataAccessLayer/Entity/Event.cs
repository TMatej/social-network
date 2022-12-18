using DataAccessLayer.Entity.JoinEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class Event : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public Group Group { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public IList<EventParticipant> EventParticipants { get; set; }
    }
}
