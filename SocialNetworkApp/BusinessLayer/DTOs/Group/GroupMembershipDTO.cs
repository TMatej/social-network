using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Group
{
    public class GroupMembershipDTO
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int MembershipTypeId { get; set; }
    }
}
