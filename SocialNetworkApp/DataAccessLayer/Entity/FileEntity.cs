using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class FileEntity : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
