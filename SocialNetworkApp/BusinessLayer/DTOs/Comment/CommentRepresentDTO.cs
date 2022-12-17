using BusinessLayer.DTOs.User;

namespace BusinessLayer.DTOs.Comment
{
    public class CommentRepresentDTO
    {
        public int Id { get; set; }
        public IList<CommentBasicRepresentDTO> Comments { get; set; }
        public int CommentableId { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
    }
}
