using BusinessLayer.Contracts;
using DataAccessLayer.Entity;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services
{
    internal class CommentService : GenericService<Comment>, ICommentService
    {
        private readonly IQuery<Comment> commentQuery;
        public CommentService(IRepository<Comment> repository, IUnitOfWork uow, IQuery<Comment> commentQuery) : base(repository, uow)
        {
            this.commentQuery = commentQuery;
        }

        public List<Comment> getCommentsForEntity(int entityId, int page = 1, int pageSize = 10)
        {
            return commentQuery
                .Where<int>(id => id == entityId, "CommentableId")
                .Page(page, pageSize)
                .OrderBy<DateTime>("CreatedAt")
                .Execute()
                .Items
                .ToList();
        }
    }
}
