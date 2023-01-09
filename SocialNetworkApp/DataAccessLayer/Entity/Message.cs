using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class Message : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        // maybe ConversationParticipant instead of User?
        // and remove receiver for group chat
        [ForeignKey(nameof(ReceiverId))]
        public User Receiver { get; set; }

        [Required]
        public int AuthorId { get; set; }

        // maybe ConversationParticipant instead of User?
        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int ConversationId { get; set; }

        [ForeignKey(nameof(ConversationId))]
        public Conversation Conversation { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public IEnumerable<Attachment> Attachments { get; set; }
    }
}
