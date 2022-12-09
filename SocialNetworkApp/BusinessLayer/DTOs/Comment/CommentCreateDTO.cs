namespace BusinessLayer.DTOs.Comment
{
    public class CommentCreateDTO
    {
        public int CommentableId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
    }
}
