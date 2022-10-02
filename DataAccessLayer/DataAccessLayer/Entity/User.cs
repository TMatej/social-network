using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    [Table("User")]
    public class User
    {
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("Username")]
        [MinLength(4)]
        [MaxLength(64)]
        [Required]
        public string Username { get; set; }

        [Column("PrimaryEmail")]
        [MaxLength(255)]
        [Required]
        public string PrimaryEmail { get; set; }

        [Column("SecondaryEmail")]
        [MaxLength(255)]
        public string? SecondaryEmail { get; set; }

        [Column("PasswordHash")]
        [Required]
        public string PasswordHash { get; set; }

        public Profile Profile { get; set; }
        // more properties and associations will follow
    }
}
