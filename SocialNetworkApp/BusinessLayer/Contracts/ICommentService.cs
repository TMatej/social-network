using DataAccessLayer.Entity;

namespace BusinessLayer.Contracts
{
    public interface ICommentService
    {
        public List<Comment> getCommentsForEntity(int entityId, int page = 1, int pageSize = 10);
    }
}
