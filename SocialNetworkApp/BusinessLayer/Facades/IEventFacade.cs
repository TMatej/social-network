using BusinessLayer.DTOs.Event;
using BusinessLayer.DTOs.Group;
using BusinessLayer.DTOs.User;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades
{
    public interface IEventFacade
    {
        public IEnumerable<EventRepresentDTO> FindByName(string name);
        public IEnumerable<EventRepresentDTO> FindByCreator(UserDTO creatorDTO);
        public IEnumerable<EventRepresentDTO> FindByGroup(GroupRepresentDTO groupDTO);
        public void AddParticipant(EventParticipationDTO eventParticipationDTO);
        public void RemoveParticipant(EventParticipationDTO eventParticipationDTOO);

        public void CreateEvent(EventCreateDTO eventCreateDTO);

        public void UpdateEvent(EventRepresentDTO eventRepresentDTO);

        public void DeleteEvent(EventRepresentDTO eventRepresentDTO);

        public EventRepresentDTO? GetEvent(int id);
    }
}
