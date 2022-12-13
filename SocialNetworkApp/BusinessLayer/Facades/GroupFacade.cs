using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Event;
using BusinessLayer.DTOs.Group;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Facades
{
    public class GroupFacade : IGroupFacade
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupFacade(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        public void CreateGroup(GroupCreateDTO groupCreateDTO)
        {
            var group = new Group
            {
                Name = groupCreateDTO.Name,
                Description = groupCreateDTO.Description,
            };
            _groupService.Insert(group);
        }

        public void UpdateGroup(GroupRepresentDTO groupRepresentDTO)
        {
            var group = _mapper.Map<Group>(groupRepresentDTO);
            _groupService.Update(group);
        }

        public void DeleteGroup(GroupRepresentDTO groupRepresentDTO)
        {
            _groupService.Delete(groupRepresentDTO.Id);
        }

        public void AddToGroup(GroupMembershipDTO groupMembershipDTO)
        {
            _groupService.AddToGroup(groupMembershipDTO.GroupId, groupMembershipDTO.UserId, groupMembershipDTO.MembershipTypeId);
        }

        public bool RemoveFromGroup(GroupMembershipDTO groupMembershipDTO)
        {
            return _groupService.RemoveFromGroup(groupMembershipDTO.GroupId, groupMembershipDTO.UserId);
        }

        public GroupRepresentDTO? GetGroup(int id)
        {
            var group = _groupService.GetByID(id);
            
           return group==null ? null : _mapper.Map<GroupRepresentDTO>(group);
        }
    }
}
