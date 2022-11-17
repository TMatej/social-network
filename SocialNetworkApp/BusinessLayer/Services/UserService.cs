using BusinessLayer.Contracts;
using BusinessLayer.DTOs;
using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        public readonly IRepository<User> userRepository;
        private IUnitOfWork uow;

        public UserService(IRepository<User> repository, IUnitOfWork uow)
        {
            this.userRepository = repository;
            this.uow = uow;
        }

        public async Task Register(RegisterDTO registerDTO)
        {
            User user = new User
            {
                Username = registerDTO.Username,
                // TODO: add hashing
                PasswordHash = registerDTO.Password,
                PrimaryEmail = registerDTO.Email,
            };

            userRepository.Insert(user);
            uow.Commit(); // always neccessary to call iow.Commit() to persist the data into DB
        }
    }
}
