using DataAccessLayer.Entity.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity
{
    public class Profile : Postable
    {
        [MaxLength(64)]
        public string? Name { get; set; }

        public Address? Address { get; set; }

        public Sex Sex { get; set; }

        public List<Galery>? Galeries { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }        

        public IList<Post> Posts { get; set; }
    }
}
