
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.DTOs.User
{
    public class UpdateUserAvatarDTO
    {
        public IFormFile avatar { get; set; }
    }
}
