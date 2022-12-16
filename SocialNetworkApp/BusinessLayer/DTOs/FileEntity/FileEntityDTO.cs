
namespace BusinessLayer.DTOs.User
{
    public class FileEntityDTO
    {
        public Guid Guid { get; set; }

        public byte[] Data { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
