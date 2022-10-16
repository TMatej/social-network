using DataAccessLayer.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    public class Profile : Postable
    {
        [MaxLength(64)]
        [Column("Name")]
        public string? Name { get; set; }

        [Column("Address")]
        public Address? Address { get; set; }

        [Column("Sex")]
        public Sex Sex { get; set; }

        public List<Galery>? Galeries { get; set; }

        [Column("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [Column("DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        [Column("CreatedAt")]
        [Required]
        public DateTime CreatedAt { get; set; }

        
        [Required]
        public User Owner { get; set; }
        
        [Column("OwnerId")]
        [Required]
        public int OwnerId { get; set; }
        

        // public List<Post> Posts { get; set; }
    }
}
