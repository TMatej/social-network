using BusinessLayer.DTOs.Group;
using BusinessLayer.DTOs.Search;
using BusinessLayer.DTOs.User;

namespace BusinessLayer.DTOs.Event
{
    public class EventRepresentDTO : SearchableDTO
    {
        public UserDTO User { get; set; }
        public GroupRepresentDTO Group { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
