using DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IGroupService
    {
        public IEnumerable<Group> GetByUser(User user);
        public void AddToGroup(User user, Group group, GroupRole groupRole);
        public void RemoveFromGroup(User user, Group group);
    }
}
