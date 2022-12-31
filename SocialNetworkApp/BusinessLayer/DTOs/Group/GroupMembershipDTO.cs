using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity.Enum;

namespace BusinessLayer.DTOs.Group
{
    public class GroupMembershipDTO
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public UserDTO? User { get; set; }
        public GroupRole GroupRole { get; set; }
    }
}
