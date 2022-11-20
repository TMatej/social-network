using DataAccessLayer.Entity;


namespace BusinessLayer.Contracts
{
    public interface IEventService
    {
        public IEnumerable<Event> FindByName(string name);
        public IEnumerable<Event> FindByParticipant(User participant);
        public IEnumerable<Event> FindByCreator(User creator);
        public IEnumerable<User> FindAllParticipants(Event _event);
        public IEnumerable<User> FindParticipatingFriends(User participant, Event _event);
        public IEnumerable<Event> FindByGroup(Group group);
        public void AddParticipant(User user, Event _event, ParticipationType participationType);
        public void RemoveParticipant(User user, Event _event);


    }
}
