using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using DataAccessLayer.Entity.JoinEntity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class GroupService : GenericService<Group>, IGroupService
    {
       
        public readonly IRepository<GroupMember> groupMemberRepository;

        public GroupService(IRepository<Group> repository, IRepository<GroupMember> groupMemberRepository, IUnitOfWork uow) : base(repository, uow)
        {
            this.groupMemberRepository = groupMemberRepository;
        }

        public IEnumerable<Group> GetByUser(User user)
        {
            var groups = _repository.GetAll().Where(g => g.GroupMembers.Select(m => m.UserId).Contains(user.Id));
            return groups;
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
            var groupMember = groupMemberRepository.GetAll().Where(m => m.GroupId == group.Id && m.UserId == user.Id).FirstOrDefault();
            if (groupMember != null)
            {
                groupMemberRepository.Delete(groupMember);
                _uow.Commit();
            }
        }
    }
}
