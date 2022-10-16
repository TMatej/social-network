using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(64)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(255)]
        public string PrimaryEmail { get; set; }

        [MaxLength(255)]
        public string? SecondaryEmail { get; set; }

        public Profile Profile { get; set; }
    }
}
