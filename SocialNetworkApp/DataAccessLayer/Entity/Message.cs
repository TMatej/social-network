using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class Message : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ConversationId { get; set; }

        [Required]
        public Conversation Conversation { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public User Author { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public int? AttachmentId { get; set; }

        public Attachment Attachment { get; set; }
    }
}
