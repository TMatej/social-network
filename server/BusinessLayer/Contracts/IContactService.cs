using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;

namespace BusinessLayer.Contracts
{
    public interface IContactService : IGenericService<Contact>
    {
        public void AddContact(int userId, int addedContactUserId);
        public IEnumerable<User> GetContacts(int userId);
        public void RemoveContact(int user1Id, int user2Id);
    }
}
