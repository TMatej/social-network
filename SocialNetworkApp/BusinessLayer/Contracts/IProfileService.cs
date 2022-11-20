using BusinessLayer.DTOs.Post;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Contracts
{
    public interface IProfileService : IGenericService<Profile>
    {
        public void addPost(int profileId, int userId, PostCreateDTO postDTO);
        public void changeAvatar(int profileId, IFormFile avatar);
    }
}
