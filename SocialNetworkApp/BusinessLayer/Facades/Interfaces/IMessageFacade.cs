using BusinessLayer.DTOs.Message;
using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IMessageFacade
    {
        public IEnumerable<MessageRepresentDTO> GetDirectMessagesBetween(int user1Id, int user2Id, int page = 1, int pageSize = 10);
        public IEnumerable<UserDTO> GetDirectMessagesContacts(int userId, int page = 1, int pageSize = 10);

        public void CreateMessage (MessageCreateDTO messageDTO);
    }
}
