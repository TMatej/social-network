using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System.Linq.Expressions;
using Event = DataAccessLayer.Entity.Event;

namespace ServiceTests
{
    public class EventServiceTest
    {
        IRepository<Event> eventRepository;
        IQuery<Event> eventQuery;
        IQuery<EventParticipant> participantQuery;
        IRepository<EventParticipant> participantRepository;
        IUnitOfWork uow;
        Event mockEvent;
        User mockParticipant1;
        User mockParticipant2;
        User mockCreator;
        Group group;
        EventParticipant mockEventParticipant1;
        EventParticipant mockEventParticipant2;
        ParticipationType participationType;

        [SetUp]
        public void Setup()
        {
            eventRepository = Substitute.For<IRepository<Event>>();
            eventQuery = MockQuery.CreateMockQuery<Event>();
            participantRepository = Substitute.For<IRepository<EventParticipant>>();
            participantQuery = MockQuery.CreateMockQuery<EventParticipant>();
            uow = Substitute.For<IUnitOfWork>();
            participationType = Substitute.For<ParticipationType>();
            mockParticipant1 = new User()
            {
                Id = 1,
                Username = "Participant1",
                Contacts = new List<Contact>() { new Contact() { User1Id = 1, User2Id = 3 } }
            };
            mockParticipant2 = new User()
            {
                Id = 3,
                Username = "Participant2",

            };
            mockCreator = new User()
            {
                Id = 2,
                Username = "Creator",

            };
            mockEventParticipant1 = new EventParticipant()
            {
                UserId = 1,
                EventId = 1,
                User = mockParticipant1
            };
            mockEventParticipant2 = new EventParticipant()
            {
                UserId = 3,
                EventId = 1,
                User = mockParticipant2
            };
            group = new Group()
            {
                Id = 1,
                Name = "Test",
            };
            mockEvent = new Event()
            {
                Id = 1,
                Title = "Test",
                Description = "Test",
                UserId = 2,
                User = mockCreator,
                GroupId = 1,
                Group = group,
                EventParticipants = new List<EventParticipant>()
                {
                    mockEventParticipant1,
                    mockEventParticipant2
                }
            };


            participationType.Id.Returns(1);

        }

        [Test]
        public void FindByCreator()
        {
            var eventService = new EventService(eventQuery, participantQuery, eventRepository, participantRepository, uow);
            eventService.FindByCreator(mockCreator);
            eventQuery.Received().Where(Arg.Any<Expression<Func<int, bool>>>(), "UserId");
            eventQuery.Received().Execute();
        }

        [Test]
        public void FindByTitle()
        {
            var eventService = new EventService(eventQuery, participantQuery, eventRepository, participantRepository, uow);
            eventService.FindByName("Test");
            eventQuery.Received().Where(Arg.Any<Expression<Func<string, bool>>>(), "Title");
            eventQuery.Received().Execute();
        }

        [Test]
        public void FindByGroup()
        {
            var eventService = new EventService(eventQuery, participantQuery, eventRepository, participantRepository, uow);
            eventService.FindByGroup(group);
            eventQuery.Received().Where(Arg.Any<Expression<Func<int, bool>>>(), "GroupId");
            eventQuery.Received().Execute();
        }

        [Test]
        public void AddParticipant()
        {
            var eventService = new EventService(eventQuery, participantQuery, eventRepository, participantRepository, uow);
            eventService.AddParticipant(mockParticipant2.Id, mockEvent.Id, participationType.Id);
            participantRepository.Received().Insert(Arg.Is<EventParticipant>(x => x.EventId == mockEvent.Id && x.UserId == mockParticipant2.Id && x.ParticipationTypeId == participationType.Id));
            uow.Received().Commit();
        }
        [Test]
        public void RemoveParticipant()
        {
            var result = new QueryResult<EventParticipant>(1, 1, 1, new List<EventParticipant> { mockEventParticipant1 });
            var queryWithResult = MockQuery.CreateMockQueryWithResult(result);
            var eventService = new EventService(eventQuery, queryWithResult, eventRepository, participantRepository, uow);
            eventService.RemoveParticipant(mockParticipant1.Id, mockEvent.Id);
            participantRepository.Received().Delete(Arg.Is<EventParticipant>(x => x.EventId == mockEvent.Id && x.UserId == mockParticipant1.Id));
            uow.Received().Commit();

        }
    }
}
