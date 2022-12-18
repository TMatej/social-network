using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IGroupService:IGenericService<Group>
    {
        public void AddToGroup(int groupId, int userId, int roleId);
        public bool RemoveFromGroup(int userId, int groupId);
        public IEnumerable<Group> FindByName(string name);
        public IEnumerable<Group> FindByName(string name, int pageSize, int page);
    }
}
