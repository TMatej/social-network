using BusinessLayer.DTOs.User;

namespace BusinessLayer.DTOs.Post
{
    public class PostRepresentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostableId { get; set; }
        public UserDTO User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
