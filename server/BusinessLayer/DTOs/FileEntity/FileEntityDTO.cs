
namespace BusinessLayer.DTOs.FileEntity
{
    public class FileEntityDTO
    {
        public Guid Guid { get; set; }

        public string FileType { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
