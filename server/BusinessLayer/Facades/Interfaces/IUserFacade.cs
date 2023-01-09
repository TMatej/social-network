using BusinessLayer.DTOs.Group;
using BusinessLayer.DTOs.User;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IUserFacade
    {
        UserDTO Login(UserLoginDTO userLoginDTO);
        void Register(UserRegisterDTO userRegisterDTO);
        public UserDTO GetUserFromCookieAuthId(int id);
        public void UpdateUserAvatar(int userId, IFormFile avatar);
        public IEnumerable<UserDTO> GetAllUsersPaginated(int page, int size);
        void DeleteUser(int userId);
        public IEnumerable<GroupRepresentDTO> GetGroupsForUser(int userId);

        public bool CheckPermission(string claimId, int userId);
    }
}
