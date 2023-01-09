using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Event;
using BusinessLayer.DTOs.Group;
using BusinessLayer.DTOs.User;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.Enum;

namespace BusinessLayer.Facades
{
    public class EventFacade : IEventFacade
    {
        IEventService eventService;
        IUserService userService;
        IMapper mapper;
        public EventFacade(IEventService eventService, IUserService userService, IMapper mapper)
        {
            this.eventService = eventService;
            this.userService = userService;
            this.mapper = mapper;
        }
            public void CreateEvent(EventCreateDTO eventCreateDTO)
        {
            var _event = new Event()
            {
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
            var events = eventService.Find(name);
            return events.Select(e => mapper.Map<EventRepresentDTO>(e));
        }

        public bool RemoveParticipant(EventParticipationDTO eventParticipationDTO)
        {
            return eventService.RemoveParticipant(eventParticipationDTO.UserId, eventParticipationDTO.EventId);
        }
        public void AddParticipant(EventParticipationDTO eventParticipationDTO)
        {
            eventService.AddParticipant(eventParticipationDTO.UserId, eventParticipationDTO.EventId, eventParticipationDTO.ParticipationTypeId);
        }
        
        public void UpdateEvent(EventRepresentDTO eventRepresentDTO)
        {
            var _event = mapper.Map<Event>(eventRepresentDTO);
            eventService.Update(_event);
        }

        public EventRepresentDTO? GetEvent(int id)
        {
            var _event = eventService.GetByID(id);
            return _event == null ? null : mapper.Map<EventRepresentDTO>(_event);
        }

        public bool CheckPermission(string claimId, int eventId)
        {
            var _event = eventService.GetByID(eventId);
            var owner = _event.UserId;
            var claimInt = int.Parse(claimId);
            {
                return owner == claimInt || userService.IsAdmin(claimInt);
            }
        }
    }
}
