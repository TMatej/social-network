﻿using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entity
{
    public class Conversation : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId;

        public User User { get; set; }

        public IList<ConversationParticipant> ConversationParticipants { get; set; }
    }
}
