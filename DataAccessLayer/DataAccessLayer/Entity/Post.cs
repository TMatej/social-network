using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    [Table("Post")]
    internal class Post: Commentable
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int PostableId { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(64)]
        public string Title { get; set; }
    
        [Required]
        [MinLength(4)]
        [MaxLength(256)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
