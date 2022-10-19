using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class Attachment : IEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        public int MessageId { get; set; }

        [Required]
        public Message Message { get; set; }
        
        [Required]
        public string Url { get; set; }
    }
}
