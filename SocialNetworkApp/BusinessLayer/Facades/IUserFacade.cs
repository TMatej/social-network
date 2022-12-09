using BusinessLayer.DTOs.User;

namespace BusinessLayer.Facades
{
    public interface IUserFacade
    {
        UserDTO Login(UserLoginDTO userLoginDTO);
        void Register(UserRegisterDTO userRegisterDTO);
    }
}
