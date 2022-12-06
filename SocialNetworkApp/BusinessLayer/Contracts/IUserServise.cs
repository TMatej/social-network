using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IUserService : IGenericService<User>
    {
        public void Register(UserRegisterDTO registerDTO);
        public void AddContacts(int userId, List<int> contactIds);
        public UserDTO Authorize(UserLoginDTO userLoginDTO);
    }
}
