using BusinessLayer.DTOs.Event;
using BusinessLayer.DTOs.Group;
using BusinessLayer.DTOs.User;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IEventFacade
    {
        public IEnumerable<EventRepresentDTO> FindByName(string name);
        public IEnumerable<EventRepresentDTO> FindByCreator(UserDTO creatorDTO);
        public IEnumerable<EventRepresentDTO> FindByGroup(GroupRepresentDTO groupDTO);
        public void AddParticipant(EventParticipationDTO eventParticipationDTO);
        public bool RemoveParticipant(EventParticipationDTO eventParticipationDTOO);

        public void CreateEvent(EventCreateDTO eventCreateDTO);

        public void UpdateEvent(EventRepresentDTO eventRepresentDTO);

        public void DeleteEvent(EventRepresentDTO eventRepresentDTO);

        public EventRepresentDTO? GetEvent(int id);

        public bool CheckPermission(string claimId, int eventId);
    }
}
