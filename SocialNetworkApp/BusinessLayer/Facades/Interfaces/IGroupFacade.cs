using BusinessLayer.DTOs.Group;
using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
