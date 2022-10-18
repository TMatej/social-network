using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    [Table("Attachment")]
    class Attachment
    {
        [Key]
        [Column("AttachmentId")]
        public int Id { get; set; }

        [Column("MessageId")]
        [Required]
        public int MessageId { get; set; }
        [Required]
        public Message Message { get; set; }
        [Column("Url")]
        [Required]
        public string Url { get; set; }
    }
}
