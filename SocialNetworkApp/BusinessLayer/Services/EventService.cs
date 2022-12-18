using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System.Drawing.Printing;

namespace BusinessLayer.Services
{
    public class EventService : GenericService<Event>, IEventService
    {
        readonly IRepository<EventParticipant> participantRepository;
        readonly IQuery<EventParticipant> participantQuery;
        readonly IQuery<Event> eventQuery;
        public EventService(IQuery<Event> eventQuery, IQuery<EventParticipant> participantQuery, IRepository<Event> repository, IRepository<EventParticipant> participantRepository, IUnitOfWork uow) : base(repository, uow)
        {
            this.eventQuery = eventQuery;
            this.participantRepository = participantRepository;
            this.participantQuery = participantQuery;
        }

        public IEnumerable<Event> FindByCreator(User creator)
        {
            var query = eventQuery.Where<int>(id => id == creator.Id, nameof(Event.UserId));
            var executed = query.Execute();
            var items = executed.Items;
            return items;
        }

        public IEnumerable<Event> FindByGroup(Group group)
        {
            return eventQuery.Where<int>(id => id == group.Id, nameof(Event.GroupId)).Execute().Items;
        }
        public void AddParticipant(int userId, int eventId, int participationTypeId)
        {
            {
                var participant = new EventParticipant()
                {
                    EventId = eventId,
                    ParticipationTypeId = participationTypeId,
                    UserId = userId
                };

                participantRepository.Insert(participant);
            }
            _uow.Commit();
        }
        public bool RemoveParticipant(int userId, int eventId)
        {
            var participant = participantQuery.Where<int>(eId => eId == eventId,nameof(EventParticipant.EventId)).Where<int>(uId => uId == userId, nameof(EventParticipant.UserId)).Execute().Items.FirstOrDefault();
            if (participant != null)
            {
                participantRepository.Delete(participant);
                _uow.Commit();
                return true;
            }
            return false;
        }

        private IQuery<Event> FindQuery(string name)
        {
            return eventQuery.Where<string>(n => n.Contains(name, StringComparison.CurrentCultureIgnoreCase), nameof(Event.Name));
        }

        public IEnumerable<Event> Find(string name, int pageSize, int page)
        {
            var query = FindQuery(name);
            return query.Page(page, pageSize).Execute().Items;
        }

        public IEnumerable<Event> Find(string name)
        {
            var query = FindQuery(name);
            return query.Execute().Items;
        }
    }
}
