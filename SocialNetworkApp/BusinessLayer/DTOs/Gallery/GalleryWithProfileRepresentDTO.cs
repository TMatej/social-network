using BusinessLayer.DTOs.Profile;

namespace BusinessLayer.DTOs.Gallery
{
    public class GalleryWithProfileRepresentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ProfileBasicRepresentDTO Profile { get; set; }
    }
}
