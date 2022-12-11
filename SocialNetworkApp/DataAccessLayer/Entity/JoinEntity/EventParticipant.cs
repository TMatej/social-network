using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity.JoinEntity
{
    public class EventParticipant : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int? ParticipationTypeId { get; set; }

        [ForeignKey(nameof(ParticipationTypeId))]
        public ParticipationType ParticipationType { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
