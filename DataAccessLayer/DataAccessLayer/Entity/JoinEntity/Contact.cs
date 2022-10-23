using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity.JoinEntity
{
    public class Contact : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int User1Id { get; set; }

        [ForeignKey(nameof(User1Id))]
        public User User1 { get; set; }

        [Required]
        public int User2Id { get; set; }

        [ForeignKey(nameof(User2Id))]
        public User User2 { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}