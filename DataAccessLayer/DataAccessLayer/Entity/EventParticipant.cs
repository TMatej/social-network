using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class EventParticipant
    {
        [Required]
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int ParticipationTypeId { get; set; }

        [ForeignKey(nameof(ParticipationTypeId))]
        public ParticipationType ParticipationType { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
