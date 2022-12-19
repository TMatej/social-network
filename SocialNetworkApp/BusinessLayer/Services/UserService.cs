using Ardalis.GuardClauses;
using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IRepository<User> userRepo;
        private readonly IQuery<EventParticipant> eventParticipantQuery;
        private readonly IQuery<ConversationParticipant> conversationParticipantQuery;
        private readonly IQuery<Contact> contactQuery;
        private readonly IQuery<User> userQuery;
        private readonly IRepository<Contact> contactRepo;
        private readonly IRepository<DataAccessLayer.Entity.Profile> profileRepo;
        public readonly IFileService fileService;
        public IMapper mapper;

        public UserService(IRepository<User> userRepo,
            IQuery<EventParticipant> eventParticipantQuery,
            IRepository<DataAccessLayer.Entity.Profile> profileRepo,
            IQuery<ConversationParticipant> conversationParticipantQuery,
            IQuery<Contact> contactQuery,
            IQuery<User> userQuery,
            IFileService fileService,
            IMapper mapper,
            IRepository<Contact> contactRepo,
            IUnitOfWork uow) : base(userRepo, uow)
        {
            this.userRepo = userRepo;
            this.profileRepo = profileRepo;
            this.eventParticipantQuery = eventParticipantQuery;
            this.conversationParticipantQuery = conversationParticipantQuery;
            this.contactQuery = contactQuery;
            this.contactRepo = contactRepo;
            this.userQuery = userQuery;
            this.mapper = mapper;
            this.fileService = fileService;
        }

        // Real auth implementation after shown to us on lectures
        public void Register(UserRegisterDTO registerDTO)
        {
            if (registerDTO.Password != registerDTO.RepeatPassword)
            {
                throw new Exception("Passwords do not match");
            }

            var passwordHash = Argon2.Hash(registerDTO.Password);

            DataAccessLayer.Entity.Profile profile = new DataAccessLayer.Entity.Profile
            {
                Name = registerDTO.Username,
            };

            User user = new User
            {
                Username = registerDTO.Username,
                PasswordHash = passwordHash,
                Email = registerDTO.Email,
                Profile = profile,
            };

            userRepo.Insert(user);
            _uow.Commit(); // always neccessary to call iow.Commit() to persist the data into DB
        }

        public override void Delete(object id)
        {
            Guard.Against.Null(id);
            var user = userRepo.GetByID(id);

            // remove all conversationParticipations
            // remove all eventParticipations
            // remove all contacts

            var contacts = contactQuery
                .Where<int>(x => x == user.Id, "User1Id")
                .Execute();

            foreach (var contact in contacts.Items)
            {
                contactRepo.Delete(contact.Id);
            }

            userRepo.Delete(id);
            _uow.Commit();
        }

        public void ChangeAvatar(int userId, IFormFile avatar)
        {
            Guard.Against.Null(userId);

            var avatarFileEntity = fileService.CreateFile(avatar);
            var user = GetByID(userId);
            user.Avatar = avatarFileEntity;
            Update(user);
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
            var user = userQuery.Where<string>(e => e == userLoginDTO.Email, nameof(User.Email))
              .Include(nameof(User.UserRoles))
              .Include(nameof(User.Avatar))
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

        public IEnumerable<User> FindByName(string name) {
            var query = FindQuery(name);
            return query.Include(nameof(User.Avatar)).Execute().Items;
        }
        public IEnumerable<User> FindByName(string name,int pageSize, int page)
        {
            var query = FindQuery(name);
            return query.Include(nameof(User.Avatar)).Page(page,pageSize).Execute().Items;
        }
        private IQuery<User> FindQuery(string name) { 
            return userQuery.Where<string>(username => username.ToLower().Contains(name.ToLower()), nameof(User.Username));
        }

        public UserDTO GetByIdDetailed(int userId)
        {
            var user = userQuery.Where<int>(id => id == userId, nameof(User.Id))
              .Include(nameof(User.UserRoles))
              .Include(nameof(User.Avatar))
              .Execute()
              .Items
              .FirstOrDefault();

            return mapper.Map<UserDTO>(user);
        }
    }
}
