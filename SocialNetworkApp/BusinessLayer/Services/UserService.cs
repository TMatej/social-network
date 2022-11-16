using BusinessLayer.Contracts;
using BusinessLayer.DTOs;
using DataAccessLayer.Entity;
using Infrastructure.EFCore.UnitOfWork;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        public readonly IRepository<User> repository;
        private IUnitOfWork iow;

        public UserService(IRepository<User> repository, IUnitOfWork iow)
        {
            this.repository = repository;
            this.iow = iow;
        }

        public async Task Register(RegisterDTO registerDTO)
        {
            User user = new User
            {
                Username = registerDTO.Username,
                PasswordHash = registerDTO.Password,
                PrimaryEmail = registerDTO.Email,
            };

            repository.Insert(user);
            await iow.Commit();
        }
    }
}
