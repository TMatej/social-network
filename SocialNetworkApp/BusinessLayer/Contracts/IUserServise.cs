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
        public void ChangeAvatar(int userId, IFormFile avatar);
        public IEnumerable<User> FindByName(string name);
        public IEnumerable<User> FindByName(string name, int pageSize, int page);
        public UserDTO GetByIdDetailed(int userId);
    }
}
