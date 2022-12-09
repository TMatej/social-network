using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface IGroupService
    {
        public void AddToGroup(User user, Group group, GroupRole groupRole);
        public void RemoveFromGroup(User user, Group group);
    }
}
