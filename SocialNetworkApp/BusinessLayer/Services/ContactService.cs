using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    internal class ContactService : GenericService<Contact>
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
