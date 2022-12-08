using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Contracts
{
    public interface IUserService : IGenericService<User>
    {
        public void Register(UserRegisterDTO registerDTO);
        public void AddContacts(int userId, List<int> contactIds);
        public UserDTO AuthenticateUser(UserLoginDTO userLoginDTO);
        public void changeAvatar(int userId, IFormFile avatar);
    }
}
