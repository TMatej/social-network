using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IGroupService:IGenericService<Group>
    {
        public void AddToGroup(int groupId, int userId, int roleId);
        public void RemoveFromGroup(int userId, int groupId);
    }
}
