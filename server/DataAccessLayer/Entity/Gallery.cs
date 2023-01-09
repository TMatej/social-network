using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class Gallery : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(64)]
        public string Title { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(ProfileId))]
        public Profile Profile { get; set; }

        [Required]
        public int ProfileId { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
