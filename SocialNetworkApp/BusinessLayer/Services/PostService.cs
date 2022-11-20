using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class PostService : GenericService<Post>, IPostService
    {
        public PostService(IRepository<Post> repository, IUnitOfWork uow) : base(repository, uow)
        {
        }
    }
}
