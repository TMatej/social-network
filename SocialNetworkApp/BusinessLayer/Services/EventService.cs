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
    internal class EventService :  IEventService
    {

        IRepository<Event> eventRepository;
        IRepository<User> userRepository;
        IUnitOfWork uow;
public EventService(IRepository<Event> eventRepository, IRepository<User> userRepository, IUnitOfWork uow)
        {
            this.eventRepository = eventRepository;
            this.userRepository = userRepository;
            this.uow = uow;
        }

        public IEnumerable<User> FindAllParticipants(Event eventDTO)
        {
            var _event = eventRepository.GetByID(eventDTO.Id);
            var participants = _event.EventParticipants.Select(p => p.User);
            return participants;

        }

        public IEnumerable<Event> FindByCreator(User creator)
        {
            var events = eventRepository.GetAll().Where(e => e.UserId == creator.Id);
            return events;
        }

        public IEnumerable<Event> FindByName(string name)
        {
            var events = eventRepository.GetAll().Where(e => e.Title.Contains(name, StringComparison.CurrentCultureIgnoreCase));
            return events;

        }

        public IEnumerable<Event> FindByParticipant(User participant)
        {
            var events = eventRepository.GetAll().Where(e => e.EventParticipants.Select(p => p.UserId).Contains(participant.Id));
            return events;
        }

        public IEnumerable<User> FindParticipatingFriends(User participant, Event _event)
        {
;
            var user = userRepository.GetByID(participant.Id);
            var participants = _event.EventParticipants.Select(p => p.User);
            var contactsID = user.Contacts.Select(c => c.User2Id);
            var friends = participants.Where(p=>contactsID.Contains(p.Id));
            return friends;
        }
    }
}
