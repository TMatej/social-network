using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class ContactService : GenericService<Contact>, IContactService
    {
        IQuery<Contact> query;
        public ContactService(IRepository<Contact> repository, IQuery<Contact> query, IUnitOfWork uow) : base(repository, uow)
        {
            this.query = query;
        }

        public void AddContact(int userId, int addedContactUserId)
        {
            var contact = new Contact()
            {
                User1Id = userId,
                User2Id = addedContactUserId,
            };

            Insert(contact);
        }


        public IEnumerable<User> GetContacts(int userId)
        {
            var contacts = query.Where<int>(id => id == userId, nameof(Contact.User1Id)).Include(nameof(Contact.User2)).Execute().Items;
            return contacts.Select(contact => contact.User2);
        }

        public void RemoveContact(int user1Id, int user2Id)
        {
            var contact = query.Where<int>(id => id == user1Id, nameof(Contact.User1Id)).Where<int>(id => id == user2Id, nameof(Contact.User2Id)).Execute().Items.Single();
            Delete(contact);

        }
    }
}
