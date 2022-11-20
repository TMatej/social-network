using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class GroupService : IGroupService
    {
        public readonly IRepository<Group> groupRepository;

        private IUnitOfWork uow;

        public GroupService(IRepository<Group> repository, IUnitOfWork uow)
        {
            groupRepository = repository;
            this.uow = uow;
        }
        public IEnumerable<Group> GetByUser(User user)
        {
            var groups = groupRepository.GetAll().Where(g => g.GroupMembers.Select(m => m.UserId).Contains(user.Id));
            return groups;
        }
    }
}
