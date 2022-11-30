using Ardalis.GuardClauses;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IRepository<User> userRepo;
        private readonly IQuery<EventParticipant> eventParticipantQuery;
        private readonly IQuery<ConversationParticipant> conversationParticipantQuery;
        private readonly IQuery<Contact> contactQuery;
        private readonly IRepository<Contact> contactRepo;

        public UserService(IRepository<User> userRepo,
            IQuery<EventParticipant> eventParticipantQuery,
            IQuery<ConversationParticipant> conversationParticipantQuery,
            IQuery<Contact> contactQuery,
            IRepository<Contact> contactRepo,
            IUnitOfWork uow) : base(userRepo, uow)
        {
            this.userRepo = userRepo;
            this.eventParticipantQuery = eventParticipantQuery;
            this.conversationParticipantQuery = conversationParticipantQuery;
            this.contactQuery = contactQuery;
            this.contactRepo = contactRepo;
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

        public void addContacts(int userId, List<int> contactIds)
        {
            foreach (var contactId in contactIds)
            {
                contactRepo.Insert(new Contact() { User1Id = userId, User2Id = contactId });
            }
        }
    }
}
