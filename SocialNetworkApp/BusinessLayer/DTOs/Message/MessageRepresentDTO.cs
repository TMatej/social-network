using BusinessLayer.DTOs.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Message
{
    public class MessageRepresentDTO
    {
        public int Id { get; set; }

        public int ReceiverId { get; set; }

        public UserDTO Receiver { get; set; }

        public int AuthorId { get; set; }

        public UserDTO Author { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? AttachmentId { get; set; }
    }
}
