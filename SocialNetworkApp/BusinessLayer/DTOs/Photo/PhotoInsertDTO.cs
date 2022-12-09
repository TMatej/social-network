namespace BusinessLayer.DTOs.Photo
{
    /* NOT FINAL A CLASS */
    public class PhotoInsertDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Url { get; set; }
        public int GalleryId { get; set; }
    }
}
