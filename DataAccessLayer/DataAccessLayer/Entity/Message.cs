using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    [Table("Message")]
    class Message
    {
        [Key]
        [Column("MessageId")]
        public int ID { get; set; }

        [Column("ConversationId")]
        [Required]
        public int ConversationID { get; set; }
        [Required]
        public Conversation Conversation { get; set; }

        [Column("AuthorId")]
        [Required]
        public int AuthorID { get; set; }
        [Required]
        public User Author { get; set; }

        [Column("Content")]
        [Required]
        public string Content { get; set; }

        [Column("Timestamp")]
        [Required]
        public string Timestamp { get; set; }
        public Attachment Attachment { get; set; }
    }
}
