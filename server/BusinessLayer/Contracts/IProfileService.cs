using BusinessLayer.DTOs.Post;
using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IProfileService : IGenericService<Profile>
    {
        public void AddPost(int profileId, int userId, PostCreateDTO postDTO);
        public Profile GetByUserId(int userId);
    }
}
