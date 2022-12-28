using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IContactFacade
    {
        public void AddContact(int userId, int addedContactUserId);
        public IEnumerable<UserDTO> GetContacts(int userId);
        public void RemoveContact(int user1Id, int user2Id);
    }
}
