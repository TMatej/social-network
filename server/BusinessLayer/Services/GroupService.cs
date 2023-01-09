using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.Enum;
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
        readonly IQuery<Group> groupQuery;

        public GroupService(IQuery<GroupMember> groupMemberQuery, IQuery<Group> groupQuery, IRepository<Group> repository, IRepository<GroupMember> groupMemberRepository, IUnitOfWork uow) : base(repository, uow)
        {
            this.groupMemberRepository = groupMemberRepository;
            this.groupMemberQuery = groupMemberQuery;
            this.groupQuery = groupQuery;
        }
        public void AddToGroup(int groupId, int userId)
        {
            var groupMember = new GroupMember
            {
                GroupId = groupId,
                UserId = userId,
                GroupRole = GroupRole.Member,
            };
            groupMemberRepository.Insert(groupMember);
            _uow.Commit();
        }

        public IEnumerable<Group> FindByName(string name)
        {
            var query = FindQuery(name);
            return query.Execute().Items;
        }

        public IEnumerable<Group> FindByName(string name, int pageSize, int page)
        {
            var query = FindQuery(name);
            return query.Page(page, pageSize).Execute().Items;
        }
        private IQuery<Group> FindQuery(string name)
        {
            return groupQuery.Where<string>(grpname => grpname.ToLower().Contains(name.ToLower()), nameof(Group.Name));
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

        public Group? GetByIdDetailed(int id) {
          return groupQuery.Where<int>(x => x == id, nameof(Group.Id))
            .Include(nameof(Group.GroupMembers), $"{nameof(Group.GroupMembers)}.{nameof(GroupMember.User)}")
            .Execute().Items.FirstOrDefault();
        }

        public IEnumerable<Group> FindGroupsForUser(int userId) {
          return groupMemberQuery.Where<int>(x => x == userId, nameof(GroupMember.UserId))
            .Include(nameof(GroupMember.Group), $"{nameof(GroupMember.Group)}.{nameof(Group.GroupMembers)}", $"{nameof(GroupMember.Group)}.{nameof(Group.GroupMembers)}.{nameof(GroupMember.User)}")
            .Execute().Items.Select(x => x.Group);
        }
    }
}
