using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Entity.JoinEntity;

namespace DataAccessLayer.Entity
{
    public class Conversation : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public IList<ConversationParticipant> ConversationParticipants { get; set; }

        public IList<Message> Messages { get; set; }
    }
}
