using BusinessLayer.DTOs.User;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IUserFacade
    {
        UserDTO Login(UserLoginDTO userLoginDTO);
        void Register(UserRegisterDTO userRegisterDTO);
        public UserDTO GetUserFromCookieAuthId(int id);
    }
}
