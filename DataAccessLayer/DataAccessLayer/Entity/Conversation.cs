using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entity
{
    [Table("Conversation")]
    class Conversation
    {
        [Key]
        [Column("ConversationId")]
        public int ID;

        [Column("OwnerId")]
        [Required]
        public int OwnerID;
        [Required]
        public User Owner { get; set; }


    }
}
