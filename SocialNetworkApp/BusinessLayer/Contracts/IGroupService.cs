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

        public IEnumerable<Group> GetByRole(GroupRole role);
    }
}
