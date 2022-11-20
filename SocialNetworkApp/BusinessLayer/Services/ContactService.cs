using BusinessLayer.Contracts;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class ContactService : GenericService<Contact>, IContactService
    {
        public ContactService(IRepository<Contact> repository, IUnitOfWork uow) : base(repository, uow)
        {
        }

        public void addContact(int userId, int addedContactUserId)
        {
            var contact = new Contact()
            {
                User1Id = userId,
                User2Id = addedContactUserId,
            };

            this.Insert(contact);
        }
    }
}
