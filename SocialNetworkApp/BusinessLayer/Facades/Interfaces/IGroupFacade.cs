using BusinessLayer.DTOs.Group;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IGroupFacade
    {
        public void CreateGroup(GroupCreateDTO groupCreateDTO);
        public GroupRepresentDTO? GetGroup(int id);
        public void UpdateGroup(GroupRepresentDTO groupRepresentDTO);
        public void DeleteGroup(GroupRepresentDTO groupRepresentDTO);
        public void AddToGroup(GroupMembershipDTO groupMembershipDTO);
        public bool RemoveFromGroup(GroupMembershipDTO groupMembershipDTO);
    }
}
