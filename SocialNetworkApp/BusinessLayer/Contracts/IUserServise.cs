using BusinessLayer.DTOs;
using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IUserService : IGenericService<User>
    {
        public Task Register(RegisterDTO registerDTO);
        public void addContacts(int userId, List<int> contactIds);
    }
}
