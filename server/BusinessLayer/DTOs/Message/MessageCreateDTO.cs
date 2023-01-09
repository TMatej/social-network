using BusinessLayer.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Message
{
    public class MessageCreateDTO
    {
        public int ReceiverId { get; set; }
        public int AuthorId { get; set; }
        public string Content { get; set; }
        public int? AttachmentId { get; set; }
    }
}
