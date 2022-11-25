using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class Post : Commentable
    {
        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int PostableId { get; set; }

        [ForeignKey(nameof(PostableId))]
        public Postable Postable { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(64)]
        public string Title { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(256)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
