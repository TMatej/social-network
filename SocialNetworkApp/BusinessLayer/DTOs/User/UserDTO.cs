using BusinessLayer.DTOs.FileEntity;
using BusinessLayer.DTOs.Search;

namespace BusinessLayer.DTOs.User
{
    public class UserDTO : SearchableDTO
    {
        public string Email { get; set; }
        public FileEntityDTO Avatar { get; set; }
        public List<string> Roles { get; set; }
    }
}
