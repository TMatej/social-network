using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    [Table("Group")]
    public class Group : Postable
    {
        [MinLength(4)]
        [MaxLength(64)]
        [Required]
        public string Name { get; set; }

        [MaxLength(255)]
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
