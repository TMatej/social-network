using BusinessLayer.DTOs.FileEntity;

namespace BusinessLayer.DTOs.Photo
{
    public class PhotoRepresentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Url { get; set; }
        public FileEntityDTO FileEntity { get; set; }
        public int GaleryId { get; set; }
    }
}
