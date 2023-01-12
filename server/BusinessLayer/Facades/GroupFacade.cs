using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Group;
using BusinessLayer.Facades.Interfaces;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.Enum;
using DataAccessLayer.Entity.JoinEntity;

namespace BusinessLayer.Facades
{
    public class GroupFacade : IGroupFacade
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GroupFacade(IGroupService groupService, IUserService userService, IMapper mapper)
        {
            _groupService = groupService;
            _userService = userService;
            _mapper = mapper;
        }

        public void CreateGroup(GroupCreateDTO groupCreateDTO, int creatorId)
        {
            var groupMember = new GroupMember() {
              UserId = creatorId,
              GroupRole = GroupRole.Author, 
            };

            var group = new Group
            {
                Name = groupCreateDTO.Name,
                Description = groupCreateDTO.Description,
                GroupMembers = new List<GroupMember>() { groupMember }
            };

            _groupService.Insert(group);
        }

        public void UpdateGroup(GroupRepresentDTO groupRepresentDTO)
        {
            var group = _mapper.Map<Group>(groupRepresentDTO);
            _groupService.Update(group);
        }

        public void DeleteGroup(int groupId)
        {
            _groupService.Delete(groupId);
        }

        public void AddToGroup(int groupId, int userId)
        {
            _groupService.AddToGroup(groupId, userId);
        }

        public bool RemoveFromGroup(int groupId, int userId)
        {
            return _groupService.RemoveFromGroup(userId, groupId);
        }

        public GroupRepresentDTO? GetGroup(int id)
        {
            var group = _groupService.GetByIdDetailed(id);
            
           return group==null ? null : _mapper.Map<GroupRepresentDTO>(group);
        }

        public IEnumerable<GroupRepresentDTO> GetGroupsForUser(int userId)
        {
            return _groupService.FindGroupsForUser(userId).Select(x => _mapper.Map<GroupRepresentDTO>(x));
        }

        public bool CheckPermission(string claimId, int groupId)
        {
            var group = _groupService.GetByIdDetailed(groupId);
            var authors = group?.GroupMembers.Where(x => x.GroupRole == GroupRole.Author).Select(x => x.UserId);
            var claimInt = int.Parse(claimId);
            {
                return authors?.Contains(claimInt) ?? false || _userService.IsAdmin(claimInt);
            }
        }
    }
}
