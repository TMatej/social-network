using DataAccessLayer.Entity;


namespace BusinessLayer.Contracts
{
    public interface IEventService
    {
        public IEnumerable<Event> FindByName(string name);
        public IEnumerable<Event> FindByCreator(User creator);
        public IEnumerable<Event> FindByGroup(Group group);
        public void AddParticipant(User user, Event _event, ParticipationType participationType);
        public void RemoveParticipant(User user, Event _event);


    }
}
