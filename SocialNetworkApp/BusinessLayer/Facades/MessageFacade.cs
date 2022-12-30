using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Message;
using BusinessLayer.DTOs.Post;
using BusinessLayer.DTOs.User;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades
{
    public class MessageFacade : IMessageFacade
    {
        private readonly IMapper mapper;
        private readonly IMessagesService messagesService;

        public MessageFacade(IMapper mapper, IMessagesService messagesService)
        {
            this.mapper = mapper;
            this.messagesService = messagesService;
        }

        public void CreateMessage(MessageCreateDTO messageDTO)
        {
            messagesService.Insert(
                new Message()
                {
                    AuthorId = messageDTO.AuthorId,
                    ReceiverId = messageDTO.ReceiverId,
                    Content = messageDTO.Content,
                    AttachmentId = messageDTO.AttachmentId
                }
                );

            
        }

        public IEnumerable<MessageRepresentDTO> GetDirectMessagesBetween(int user1Id, int user2Id, int page = 1, int pageSize = 10)
        {
            var messages = messagesService.GetDirectMessagesBetween(user1Id, user2Id, page, pageSize);
            return messages.Select(message => mapper.Map<MessageRepresentDTO>(message));
        }

        public IEnumerable<UserDTO> GetDirectMessagesContacts(int userId, int page = 1, int pageSize = 10)
        {
            var users = messagesService.GetDirectMessagesContacts(userId, page, pageSize);
            return users.Select(user => mapper.Map<UserDTO>(user));
        }
    }
}
