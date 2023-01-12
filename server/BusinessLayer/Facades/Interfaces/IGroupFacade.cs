using BusinessLayer.DTOs.Group;

namespace BusinessLayer.Facades.Interfaces
{
    public interface IGroupFacade
    {
        public void CreateGroup(GroupCreateDTO groupCreateDTO, int creatorId);
        public GroupRepresentDTO? GetGroup(int id);
        public void UpdateGroup(GroupRepresentDTO groupRepresentDTO);
        public void DeleteGroup(int groupId);
        public void AddToGroup(int groupId, int userId);
        public bool RemoveFromGroup(int groupId, int userId);
        public bool CheckPermission(string claimId, int groupId);
    }
}
