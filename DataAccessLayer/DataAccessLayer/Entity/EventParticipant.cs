using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class EventParticipant : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public Event Event { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int ParticipationTypeId { get; set; }
        
        [Required]
        public ParticipationType ParticipationType { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
