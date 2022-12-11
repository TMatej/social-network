using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Event;
using BusinessLayer.DTOs.Group;
using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades
{
    public class EventFacade : IEventFacade
    {
        IEventService eventService;
        IMapper mapper;
        public EventFacade(IEventService eventService, IMapper mapper)
        {
            this.eventService = eventService;
            this.mapper = mapper;
        }
        public void AddParticipant(UserDTO userDTO, EventRepresentDTO eventDTO, ParticipationTypeDTO participationTypeDTO)
        {
            var user = mapper.Map<User>(userDTO);
            var _event = mapper.Map<Event>(eventDTO);
            var participationType = mapper.Map<ParticipationType>(participationTypeDTO);
            eventService.AddParticipant(user, _event, participationType);
        }

        public void CreateEvent(EventCreateDTO eventCreateDTO)
        {
            var _event = new Event()
            {
                CreatedAt = DateTime.Now,
                Description = eventCreateDTO.Description,
                GroupId = eventCreateDTO.Group.Id,
                UserId = eventCreateDTO.User.Id,
                Title = eventCreateDTO.Title
            };
            eventService.Insert(_event);
        }

        public void DeleteEvent(EventRepresentDTO eventRepresentDTO)
        {
            eventService.Delete(eventRepresentDTO.Id);
        }

        public IEnumerable<EventRepresentDTO> FindByCreator(UserDTO creatorDTO)
        {
            var creator = mapper.Map<User>(creatorDTO);
            var events = eventService.FindByCreator(creator);
            return events.Select(e => mapper.Map<EventRepresentDTO>(e));
        }

        public IEnumerable<EventRepresentDTO> FindByGroup(GroupRepresentDTO groupDTO)
        {
            var group = mapper.Map<Group>(groupDTO);
            var events = eventService.FindByGroup(group);
            return events.Select(e => mapper.Map<EventRepresentDTO>(e));
        }

        public IEnumerable<EventRepresentDTO> FindByName(string name)
        {
            var events = eventService.FindByName(name);
            return events.Select(e => mapper.Map<EventRepresentDTO>(e));
        }

        public void RemoveParticipant(UserDTO userDTO, EventRepresentDTO eventDTO)
        {
            var user = mapper.Map<User>(userDTO);
            var _event = mapper.Map<Event>(eventDTO);
            eventService.RemoveParticipant(user, _event);

        }

        public void UpdateEvent(EventRepresentDTO eventRepresentDTO)
        {
            var _event = mapper.Map<Event>(eventRepresentDTO);
            eventService.Update(_event);
        }
    }
}
