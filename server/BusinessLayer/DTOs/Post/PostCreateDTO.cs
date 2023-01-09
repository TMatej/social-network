namespace BusinessLayer.DTOs.Post
{
    public class PostCreateDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int PostableId { get; set; }
    }
}
