namespace BusinessLayer.DTOs.Comment
{
    public class CommentBasicRepresentDTO
    {
        public int Id { get; set; }
        public int CommentableId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
    }
}
