using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var query = eventQuery.Where<int>(id => id == creator.Id, "UserId");
            var executed = query.Execute();
            var items = executed.Items;
            return items;
        }

        public IEnumerable<Event> FindByName(string name)
        {
            return eventQuery.Where<string>(name => name.Contains(name, StringComparison.CurrentCultureIgnoreCase), "Title").Execute().Items;
        }

        public IEnumerable<Event> FindByGroup(Group group)
        {
            return eventQuery.Where<int>(id => id == group.Id, "GroupId").Execute().Items;
        }
        public void AddParticipant(User user, Event _event, ParticipationType participationType)
        {
            {
                var participant = new EventParticipant()
                {
                    EventId = _event.Id,
                    UserId = user.Id,
                    ParticipationTypeId = participationType.Id
                };

                participantRepository.Insert(participant);
            }
            _uow.Commit();
        }
        public void RemoveParticipant(User user, Event _event)
        {
            var participant = participantQuery.Where<int>(eventId => eventId == _event.Id, "EventId").Where<int>(userId => userId == user.Id, "UserId").Execute().Items.FirstOrDefault();
            if (participant != null)
            {
                participantRepository.Delete(participant);
            }
            _uow.Commit();
        }
    }
}
