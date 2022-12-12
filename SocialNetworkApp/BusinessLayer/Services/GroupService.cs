using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class GroupService : GenericService<Group>, IGroupService
    {

        readonly IRepository<GroupMember> groupMemberRepository;
        readonly IQuery<GroupMember> groupMemberQuery;

        public GroupService(IQuery<GroupMember> groupMemberQuery, IRepository<Group> repository, IRepository<GroupMember> groupMemberRepository, IUnitOfWork uow) : base(repository, uow)
        {
            this.groupMemberRepository = groupMemberRepository;
            this.groupMemberQuery = groupMemberQuery;
        }
        public void AddToGroup(int groupId, int userId, int roleId)
        {
            var groupMember = new GroupMember
            {
                GroupId = groupId,
                UserId = userId,
                GroupRoleId = roleId
            };
            groupMemberRepository.Insert(groupMember);
            _uow.Commit();
        }
        public bool RemoveFromGroup(int userId, int groupId)
        {
            var groupMember = groupMemberQuery.Where<int>(gId => gId == groupId, nameof(GroupMember.GroupId)).Where<int>(uId => uId == userId, nameof(GroupMember.UserId)).Execute().Items.FirstOrDefault();
            if (groupMember != null)
            {
                groupMemberRepository.Delete(groupMember);
                _uow.Commit();
                return true;
            }
            return false;
        }
    }
}
