using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IMessagesService : IGenericService<Message>
    {
        public IEnumerable<Message> GetDirectMessagesBetween(int user1Id, int user2Id, int page = 1, int pageSize = 10);
        public IEnumerable<User> GetDirectMessagesContacts(int userId, int page = 1, int pageSize = 10);
    }
}
