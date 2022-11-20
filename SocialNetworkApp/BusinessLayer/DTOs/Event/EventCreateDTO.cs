using BusinessLayer.DTOs.Group;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Event
{
    public class EventDTO
    {
        public UserDTO User { get; set; }
        public GroupRepresentDTO Group { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
