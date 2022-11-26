﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Comment
{
    public class CommentEditDTO
    {
        public int Id { get; set; }
        public int CommentableId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
    }
}
