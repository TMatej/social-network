using DataAccessLayer.Entity;
using Infrastructure.Query;

namespace BusinessLayer.Contracts
{
    public interface ICommentService
    {
        public QueryResult<Comment> getCommentsForEntity(int entityId, int page = 1, int pageSize = 10);
    }
}
