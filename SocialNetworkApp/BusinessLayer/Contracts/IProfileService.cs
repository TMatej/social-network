using BusinessLayer.DTOs.Post;
using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IProfileService : IGenericService<Profile>
    {
        public void addPost(int profileId, int userId, PostCreateDTO postDTO);
        public Profile getByUserId(int userId);
    }
}
