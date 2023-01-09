using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class Attachment : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MessageId { get; set; }

        [ForeignKey(nameof(MessageId))]
        public Message Message { get; set; }

        [Required]
        public int FileEntityId { get; set; }

        [Required]
        public FileEntity FileEntity { get; set; }
    }
}
