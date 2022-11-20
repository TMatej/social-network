using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IConversationService : IGenericService<Conversation>
    {
        public void createConversation(int creatorId, List<int> participants);
        public void addParticipant(int conversationId, int userId);
        public void postMessage(int userId, int conversationId, string content, Attachment attachment);
    }
}
