using DataAccessLayer.Entity;


namespace BusinessLayer.Contracts
{
    public interface IEventService:IGenericService<Event>
    {
        public IEnumerable<Event> Find(string name);
        public IEnumerable<Event> Find(string name, int pageSize, int page);
        public IEnumerable<Event> FindByCreator(User creator);
        public IEnumerable<Event> FindByGroup(Group group);
        public void AddParticipant(int userId, int eventId, int participationTypeId);
        public bool RemoveParticipant(int userId, int eventId);


    }
}
