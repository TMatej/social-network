using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    [Table("Galery")]
    public class Galery
    {
        [Key]
        public int Id { get; set; }

        [Column("Title")]
        [StringLength(64)]
        public string Title { get; set; }

        [Column("Description")]
        [StringLength(512)]
        public string Description { get; set; }

        [Column("CreatedAt")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(ProfileId))]
        public Profile Profile { get; set; }

        [Column("ProfileId")]
        [Required]
        public int ProfileId { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
