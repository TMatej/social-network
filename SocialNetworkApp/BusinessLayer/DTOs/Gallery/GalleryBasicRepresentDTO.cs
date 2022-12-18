namespace BusinessLayer.DTOs.Gallery
{
    public class GalleryBasicRepresentDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ProfileId { get; set; }
    }
}
