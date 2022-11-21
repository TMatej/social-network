using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;


namespace ServiceTests
{
    public class EventServiceTest
    {
        IRepository<Event> eventRepository;
        IRepository<User> userRepository;
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
            userRepository = Substitute.For<IRepository<User>>();
            participantRepository = Substitute.For<IRepository<EventParticipant>>();
            uow = Substitute.For<IUnitOfWork>();
            participationType = Substitute.For<ParticipationType>();
            mockParticipant1 = new User()
            {
                Id = 1,
                Username = "Participant1",
                Contacts = new List<Contact>() { new Contact() { User1Id = 1,User2Id = 3} }
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
                    mockEventParticipant1
                }
            };
            userRepository.GetByID(1).Returns(mockParticipant1);
            userRepository.GetByID(3).Returns(mockParticipant2);
            eventRepository.GetAll().Returns(new List<Event>()
            {
                mockEvent
            });
            participantRepository.GetAll().Returns(new List<EventParticipant>()
            {
                mockEventParticipant1
            });
            participationType.Id.Returns(1);
        }
        [Test]
        public void FindByCreator()
        {
            var eventService = new EventService(userRepository, eventRepository, participantRepository, uow);
            var events = eventService.FindByCreator(mockCreator);
            Assert.That(events, Has.Exactly(1).Items);
            Assert.That(events, Has.Exactly(1).EqualTo(mockEvent));
        }
        [Test]
        public void FindByParticipant()
        {
            var eventService = new EventService(userRepository, eventRepository, participantRepository, uow);
            var events = eventService.FindByParticipant(mockParticipant1);
            Assert.That(events, Has.Exactly(1).Items);
            Assert.That(events, Has.Exactly(1).EqualTo(mockEvent));
        }
        [Test]
        public void FindByTitle()
        {
            var eventService = new EventService(userRepository, eventRepository, participantRepository, uow);
            var events = eventService.FindByName("Test");
            Assert.That(events, Has.Exactly(1).Items);
            Assert.That(events, Has.Exactly(1).EqualTo(mockEvent));
        }
        [Test]
        public void FindByPartialTitle()
        {
            var eventService = new EventService(userRepository, eventRepository, participantRepository, uow);
            var events = eventService.FindByName("es");
            Assert.That(events, Has.Exactly(1).Items);
            Assert.That(events, Has.Exactly(1).EqualTo(mockEvent));
        }
        [Test]
        public void FindByGroup()
        {
            var eventService = new EventService(userRepository, eventRepository, participantRepository, uow);
            var events = eventService.FindByGroup(group);
            Assert.That(events, Has.Exactly(1).Items);
            Assert.That(events, Has.Exactly(1).EqualTo(mockEvent));
        }
        [Test]
        public void FindParticipatingFriends()
        {
            var eventService = new EventService(userRepository, eventRepository, participantRepository, uow);
            var friends = eventService.FindParticipatingFriends(mockParticipant1, mockEvent);
            Assert.That(friends, Has.Exactly(1).Items);
            Assert.That(friends, Has.Exactly(1).EqualTo(mockParticipant2));
        }
        [Test]
        public void AddParticipant()
        {
                var eventService = new EventService(userRepository, eventRepository, participantRepository, uow);
                eventService.AddParticipant(mockParticipant2, mockEvent, participationType);
            participantRepository.Received().Insert(Arg.Is<EventParticipant>(x => x.EventId == mockEvent.Id && x.UserId == mockParticipant2.Id && x.ParticipationTypeId == participationType.Id));
            uow.Received().Commit();
        }
        [Test]
        public void RemoveParticipant()
        {
            var eventService = new EventService(userRepository, eventRepository, participantRepository, uow);
            eventService.RemoveParticipant(mockParticipant1, mockEvent);
            participantRepository.Received().Delete(Arg.Is<EventParticipant>(x => x.EventId == mockEvent.Id && x.UserId == mockParticipant1.Id));
            uow.Received().Commit();
        }
    }
}        
