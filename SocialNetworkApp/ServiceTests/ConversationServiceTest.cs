using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;


namespace ServiceTests
{
    public class ConversationServiceTest
    {
        IRepository<Conversation> conversationRepo;
        IRepository<Message> messageRepo;
        IRepository<ConversationParticipant> conversationParticipantRepo;
        IUnitOfWork uow;

        [SetUp]
        public void Setup()
        {
            conversationRepo = Substitute.For<IRepository<Conversation>>();
            messageRepo = Substitute.For<IRepository<Message>>();
            conversationParticipantRepo = Substitute.For<IRepository<ConversationParticipant>>();
            uow = Substitute.For<IUnitOfWork>();
        }

        [Test]
        public void InsertConversation()
        {
            var participants = new List<int>() { 5, 7 };
            var creatorId = 1;
            var conversationService = new ConversationService(conversationRepo, conversationParticipantRepo, messageRepo, uow);
            conversationService.createConversation(creatorId, participants);
            conversationRepo.Received().Insert(Arg.Is<Conversation>(x => x.UserId == creatorId && x.ConversationParticipants.Select(y => y.UserId).SequenceEqual(participants)));
            uow.Received().Commit();
        }

        [Test]
        public void AddParticipant()
        {
            var conversationService = new ConversationService(conversationRepo, conversationParticipantRepo, messageRepo, uow);
            conversationService.addParticipant(1, 2);
            conversationParticipantRepo.Received().Insert(Arg.Is<ConversationParticipant>(x => x.ConversationId == 1 && x.UserId == 2));
            uow.Received().Commit();
        }
        [Test]
        public void PostMessage()
        {
            var conversationService = new ConversationService(conversationRepo, conversationParticipantRepo, messageRepo, uow);
            conversationService.postMessage(1, 2, "content", null);
            messageRepo.Received().Insert(Arg.Is<Message>(x => x.AuthorId == 1 && x.ConversationId == 2 && x.Content == "content"));
            uow.Received().Commit();
        }

        [Test]
        public void PostMessageNull()
        {
            var conversationService = new ConversationService(conversationRepo, conversationParticipantRepo, messageRepo, uow);
            Assert.Throws<ArgumentNullException>(() => conversationService.postMessage(1, 2, null, null));
        }
    }
}
