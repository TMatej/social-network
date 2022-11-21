using DataAccessLayer.Entity.JoinEntity;
using DataAccessLayer.Entity;
using BusinessLayer.DTOs.Group;

namespace BusinessLayer.DTOs.Event
{
    internal class EventRepresentDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public GroupRepresentDTO Group { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
