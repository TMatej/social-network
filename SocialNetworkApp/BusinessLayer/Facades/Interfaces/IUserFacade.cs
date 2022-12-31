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
    }
}
