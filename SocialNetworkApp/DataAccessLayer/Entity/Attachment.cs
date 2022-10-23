using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class Attachment : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MessageId { get; set; }

        [Required]
        public Message Message { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
