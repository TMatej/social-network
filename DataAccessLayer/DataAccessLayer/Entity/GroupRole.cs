using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    [Table("GroupRole")]
    public class GroupRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
