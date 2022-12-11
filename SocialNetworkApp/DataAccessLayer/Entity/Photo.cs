using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class Photo : Commentable
    {
        [StringLength(64)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string Url { get; set; }

        [ForeignKey(nameof(GalleryId))]
        public Gallery Gallery { get; set; }

        [Required]
        public int GalleryId { get; set; }
    }
}
