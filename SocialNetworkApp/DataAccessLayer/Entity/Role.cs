
using DataAccessLayer.Entity.JoinEntity;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class Role : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<UserRole> UserRoles { get; set; }
    }
}
