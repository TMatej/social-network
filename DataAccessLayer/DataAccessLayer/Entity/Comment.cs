using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class Comment : Commentable
    {

        [Required]
        public int CommentableId { get; set; }

        [Required]
        public Commentable Commentable { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(512)]
        public string Content { get; set; }
    }
}
