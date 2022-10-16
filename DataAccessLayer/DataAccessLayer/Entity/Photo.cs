using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    [Table("Photo")]
    public class Photo : Commentable
    {
        [StringLength(64)]
        [Column("Title")]
        public string Title { get; set; }

        [StringLength(256)]
        [Column("Description")]
        public string Description { get; set; }

        [Column("CreatedAt")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [Column("Url")]
        [Required]
        public String Url { get; set; }

        [ForeignKey(nameof(GaleryId))]
        [Column("Galery")]
        public Galery Galery { get; set; }

        [Column("GaleryId")]
        [Required]
        public int GaleryId { get; set; }

        // public List<Comment> Comments { get; set; }
    }
}
