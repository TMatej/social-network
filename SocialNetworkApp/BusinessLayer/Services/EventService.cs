using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
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
    internal class EventService : GenericService<Event>, IEventService
    {
        readonly IRepository<User> userRepository;
        readonly IRepository<EventParticipant> participantRepository;
        public EventService(IRepository<User> userRepository, IRepository<Event> repository, IRepository<EventParticipant> participantRepository, IUnitOfWork uow) : base(repository, uow)
        {
            this.userRepository = userRepository;
            this.participantRepository = participantRepository;
        }

        public IEnumerable<User> FindAllParticipants(Event eventDTO)
        {
            var _event = _repository.GetByID(eventDTO.Id);
            var participants = _event.EventParticipants.Select(p => p.User);
            return participants;

        }

        public IEnumerable<Event> FindByCreator(User creator)
        {
            var events = _repository.GetAll().Where(e => e.UserId == creator.Id);
            return events;
        }

        public IEnumerable<Event> FindByName(string name)
        {
            var events = _repository.GetAll().Where(e => e.Title.Contains(name, StringComparison.CurrentCultureIgnoreCase));
            return events;

        }

        public IEnumerable<Event> FindByParticipant(User participant)
        {
            var events = _repository.GetAll().Where(e => e.EventParticipants.Select(p => p.UserId).Contains(participant.Id));
            return events;
        }

        public IEnumerable<User> FindParticipatingFriends(User participant, Event _event)
        {
            var user = userRepository.GetByID(participant.Id);
            var participants = _event.EventParticipants.Select(p => p.User);
            var contactsID = user.Contacts.Select(c => c.User2Id);
            var friends = participants.Where(p => contactsID.Contains(p.Id));
            return friends;
        }
        public IEnumerable<Event> FindByGroup(Group group)
        {
            var events = _repository.GetAll().Where(e => e.GroupId == group.Id);
            return events;
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
            var participant = participantRepository.GetAll().Where(p => p.EventId == _event.Id && p.UserId == user.Id).FirstOrDefault();
            if (participant != null)
            {
                participantRepository.Delete(participant);
            }
            _uow.Commit();
        }
    }
}
