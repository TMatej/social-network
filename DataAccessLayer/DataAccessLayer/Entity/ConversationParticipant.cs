using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class ConversationParticipant
    {
        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int ConversationId { get; set; }

        [ForeignKey(nameof(ConversationId))]
        public Conversation Conversation { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}