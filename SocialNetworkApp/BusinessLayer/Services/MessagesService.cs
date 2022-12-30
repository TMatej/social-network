using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class MessagesService : GenericService<Message>, IMessagesService
    {
        readonly IQuery<Message> query;

        public MessagesService(IRepository<Message> repository, IQuery<Message> query, IUnitOfWork uow) : base(repository, uow)
        {
            this.query = query;
        }

        public IEnumerable<Message> GetDirectMessagesBetween(int user1Id, int user2Id, int page = 1, int pageSize = 10)
        {
            return query.Where<int>(id1 => id1 == user1Id || id1 == user2Id, nameof(Message.AuthorId)).Where<int>(id2 => id2 == user1Id || id2 == user2Id, nameof(Message.ReceiverId)).Page(page,pageSize).OrderBy<DateTime>(nameof(Message.CreatedAt),false).Execute().Items;
        }

        public IEnumerable<User> GetDirectMessagesContacts(int userId, int page = 1, int pageSize = 10)
        {
            var recres = query.Where<int>(id => id == userId, nameof(Message.ReceiverId)).Include(nameof(Message.Author)).Include(nameof(Message.Receiver)).Execute().Items;
            var sendres = query.Where<int>(id => id == userId, nameof(Message.AuthorId)).Include(nameof(Message.Receiver)).Include(nameof(Message.Author)).Execute().Items;
            var res = recres.Concat(sendres);
            var receivers = res.Select(msg => msg.Receiver);
            var senders = res.Select(msg => msg.Author);
            var users = receivers.Concat(senders);
            return users.DistinctBy(user => user.Id).Where(user => user.Id != userId).Skip((page-1)*pageSize).Take(pageSize);
        }
    }
}
