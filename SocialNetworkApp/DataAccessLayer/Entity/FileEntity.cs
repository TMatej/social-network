using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class FileEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string FileType { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public IEnumerable<Attachment> Attachments { get; set; }
    }
}
