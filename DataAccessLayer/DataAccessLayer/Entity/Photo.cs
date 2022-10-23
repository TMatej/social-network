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
        public String Url { get; set; }

        [ForeignKey(nameof(GaleryId))]
        public Galery Galery { get; set; }

        [Required]
        public int GaleryId { get; set; }
    }
}
