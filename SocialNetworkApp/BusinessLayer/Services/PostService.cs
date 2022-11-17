using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    internal class PostService : GenericService<Post>
    {
        public PostService(IRepository<Post> repository, IUnitOfWork uow) : base(repository, uow)
        {
        }
    }
}
