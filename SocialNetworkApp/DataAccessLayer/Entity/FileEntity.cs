using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class FileEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public byte[] Data { get; set; }

        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
