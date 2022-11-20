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
        public IEnumerable<Group> GetByRole(GroupRole role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetByUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
