using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IGroupService:IGenericService<Group>
    {
        public void AddToGroup(int groupId, int userId);
        public bool RemoveFromGroup(int userId, int groupId);
        public IEnumerable<Group> FindByName(string name);
        public IEnumerable<Group> FindByName(string name, int pageSize, int page);
        public Group? GetByIdDetailed(int id);
        public IEnumerable<Group> FindGroupsForUser(int userId);
    }
}
