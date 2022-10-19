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
        [MaxLength(64)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(255)]
        public string PrimaryEmail { get; set; }

        [MaxLength(255)]
        public string? SecondaryEmail { get; set; }

        public Profile? Profile { get; set; }

        public IList<EventParticipant> EventParticipants { get; set; }

        public IList<ConversationParticipant> ConversationParticipants { get; set; }

        public IList<GroupMember> GroupMembers { get; set; }

        public IList<Contact> Contacts { get; set; }
        public IList<Contact> ContactsOf { get; set; }
    }
}
