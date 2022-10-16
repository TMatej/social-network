using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    [Table("Event")]
    public class Event
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int GroupId { get; set; }
        
        [Required]
        public Group Group { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(64)]
        public string Title { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
