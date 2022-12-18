using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.User;
using BusinessLayer.Facades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades
{
    public class ContactFacade : IContactFacade
    {
        readonly IContactService contactService;
        readonly IMapper mapper;

        public void AddContact(int userId, int addedContactUserId)
        {
            contactService.AddContact(userId, addedContactUserId);
        }

        public IEnumerable<UserDTO> GetContacts(int userId)
        {
            var contacts = contactService.GetContacts(userId);
            return contacts.Select(c => mapper.Map<UserDTO>(c));
        }

        public void RemoveContact(int user1Id, int user2Id)
        {
            contactService.RemoveContact(user1Id, user2Id);
        }
    }
}
