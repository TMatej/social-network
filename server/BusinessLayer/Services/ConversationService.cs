using Ardalis.GuardClauses;
using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class ConversationService : GenericService<Conversation>, IConversationService
    {
        private readonly IRepository<Message> messageRepo;
        private readonly IRepository<ConversationParticipant> conversationParticipantRepo;

        public ConversationService(IRepository<Conversation> conversationRepo, IRepository<ConversationParticipant> conversationParticipantRepo, IRepository<Message> messageRepo, IUnitOfWork uow) : base(conversationRepo, uow)
        {
            this.conversationParticipantRepo = conversationParticipantRepo;
            this.messageRepo = messageRepo;
        }

        public void createConversation(int creatorId, IEnumerable<int> participants)
        {
            Guard.Against.Null(creatorId);

            var participantsList = participants.Select(participantId => new ConversationParticipant()
            {
                UserId = participantId,
            }).ToList();

            var conversation = new Conversation()
            {
                UserId = creatorId,
                ConversationParticipants = participantsList,
            };

            Insert(conversation);
        }

        public void addParticipant(int conversationId, int userId)
        {
            var participant = new ConversationParticipant()
            {
                ConversationId = conversationId,
                UserId = userId,
            };

            conversationParticipantRepo.Insert(participant);
            _uow.Commit();
        }

        public void postMessage(int userId, int conversationId, string content, Attachment attachment)
        {
            Guard.Against.Null(userId);
            Guard.Against.Null(conversationId);
            Guard.Against.Null(content);

            var message = new Message()
            {
                AuthorId = userId,
                Content = content,
            };

            if (attachment != null)
            {
                message.Attachments.Append(attachment);
            }
            messageRepo.Insert(message);
            _uow.Commit();
        }
    }
}
