using DataAccessLayer.Entity;
using DataAccessLayer;
using Infrastructure.EFCore.UnitOfWork;
using Infrastructure.EFCore.Repository;
using DataAccessLayer.Entity.JoinEntity;

namespace Infrastructure.EFCore.Test
{
    [TestFixture]
    public class DeleteBehaviorTest
    {
        private SocialNetworkDBContext dbContext;
        private EFUnitOfWork unitOfWork;
        private EFGenericRepository<User> userRepository;
        private EFGenericRepository<Event> eventRepository;
        private EFGenericRepository<EventParticipant> eventParticipantRepository;

        [SetUp]
        public void Setup()
        {
            dbContext = new SocialNetworkDBContext("social-network-test-db");
            unitOfWork = new EFUnitOfWork(dbContext);
            userRepository = new EFGenericRepository<User>(unitOfWork);
            eventRepository = new EFGenericRepository<Event>(unitOfWork);
            eventParticipantRepository = new EFGenericRepository<EventParticipant>(unitOfWork);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            /*dbContext.EventParticipants.Add(new EventParticipant 
            {

            });
            unitOfWork.Commit();*/
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        /*[Test]
        public void EventParticipantDeleted_After_UserDeleted_Test()
        {
            // Arrange

            // Act
            userRepository.Delete(1);
            unitOfWork.Commit();

            // Assert
            var eventParticipant = dbContext.EventParticipants
                .Where(ep => ep.Id == 1)
                .FirstOrDefault();
            Assert.That(eventParticipant, Is.Null);
        }

        [Test]
        public void EventParticipantDeleted_After_EventDeleted_Test()
        {
            // Arrange

            // Act
            eventRepository.Delete(1);
            unitOfWork.Commit();

            // Assert
            var eventParticipant = dbContext.EventParticipants
                .Where(ep => ep.Id == 1)
                .FirstOrDefault();
            Assert.That(eventParticipant, Is.Null);
        }*/
    }
}
