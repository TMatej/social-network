using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Isopoh.Cryptography.Argon2;

namespace BusinessLayer.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        public readonly IRepository<User> userRepo;
        public readonly IRepository<Contact> contactRepo;
        public readonly IQuery<User> userQuery;
        private IUnitOfWork uow;
        public IMapper mapper;

        public UserService(IMapper mapper, IQuery<User> userQuery, IRepository<User> userRepo, IRepository<Contact> contactRepo, IUnitOfWork uow) : base(userRepo, uow)
        {
            this.userRepo = userRepo;
            this.contactRepo = contactRepo;
            this.uow = uow;
            this.userQuery = userQuery;
            this.mapper = mapper;
        }

        // Real auth implementation after shown to us on lectures
        public void Register(UserRegisterDTO registerDTO)
        {
            var passwordHash = Argon2.Hash(registerDTO.Password);

            User user = new User
            {
                Username = registerDTO.Username,
                PasswordHash = passwordHash,
                Email = registerDTO.Email,
            };

            userRepo.Insert(user);
        }

        public void AddContacts(int userId, List<int> contactIds)
        {
            foreach (var contactId in contactIds)
            {
                contactRepo.Insert(new Contact() { User1Id = userId, User2Id = contactId });
            }
        }

        public UserDTO AuthenticateUser(UserLoginDTO userLoginDTO)
        {
            var user = userQuery.Where<string>(e => e == userLoginDTO.Email, "Email")
              .Include("UserRoles")
              .Execute()
              .Items
              .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var valid = Argon2.Verify(user.PasswordHash, userLoginDTO.Password);
            if (!valid)
            {
                return null;
            }

            return mapper.Map<UserDTO>(user);
        }
    }
}
