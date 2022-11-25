using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    public class PostService : GenericService<Post>, IPostService
    {
        private readonly IQuery<Post> postQuery;
        public PostService(IRepository<Post> repository, IUnitOfWork uow, IQuery<Post> postQuery) : base(repository, uow)
        {
            this.postQuery = postQuery;
        }

        public List<Post> getPostsForEntity(int entityId, int page = 1, int pageSize = 10)
        {
            return postQuery
                .Where<int>(id => id == entityId, "PostableId")
                .Page(page, pageSize)
                .OrderBy<DateTime>("CreatedAt")
                .Execute()
                .Items
                .ToList();
        }
    }
}
