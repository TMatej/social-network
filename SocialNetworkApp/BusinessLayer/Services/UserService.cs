using BusinessLayer.Contracts;
using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        public readonly IRepository<User> userRepo;
        public readonly IRepository<Contact> contactRepo;
        private IUnitOfWork uow;

        public UserService(IRepository<User> userRepo, IRepository<Contact> contactRepo, IUnitOfWork uow) : base(userRepo, uow)
        {
            this.userRepo = userRepo;
            this.contactRepo = contactRepo;
            this.uow = uow;
        }

        // Real auth implementation after shown to us on lectures
        public async Task Register(RegisterDTO registerDTO)
        {
            User user = new User
            {
                Username = registerDTO.Username,
                // TODO: add hashing
                PasswordHash = registerDTO.Password,
                PrimaryEmail = registerDTO.Email,
            };

            userRepo.Insert(user);
            uow.Commit(); // always neccessary to call iow.Commit() to persist the data into DB
        }

        public void addContacts(int userId, List<int> contactIds)
        {
            foreach (var contactId in contactIds)
            {
                contactRepo.Insert(new Contact() { User1Id = userId, User2Id = contactId });
            }
        }
    }
}
