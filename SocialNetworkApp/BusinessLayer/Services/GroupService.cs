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
        public void AddToGroup(User user, Group group, GroupRole groupRole)
        {
            var groupMember = new GroupMember
            {
                GroupId = group.Id,
                UserId = user.Id,
                GroupRoleId = groupRole.Id
            };
            groupMemberRepository.Insert(groupMember);
            _uow.Commit();
        }
        public void RemoveFromGroup(User user, Group group)
        {
            var groupMember = groupMemberQuery.Where<int>(groupId => groupId == group.Id, "GroupId").Where<int>(userId => userId == user.Id, "UserId").Execute().Items.FirstOrDefault();
            if (groupMember != null)
            {
                groupMemberRepository.Delete(groupMember);
                _uow.Commit();
            }
        }
    }
}
