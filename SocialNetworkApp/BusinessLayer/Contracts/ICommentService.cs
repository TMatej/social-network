using BusinessLayer.DTOs.Comment;
using DataAccessLayer.Entity;
using Infrastructure.Query;

namespace BusinessLayer.Contracts
{
    public interface ICommentService : IGenericService<Comment>
    {
        void AddComment(CommentCreateDTO commentDTO);
        void EditComment(CommentEditDTO commentDTO);
        CommentRepresentDTO GetDetailedCommentById(int id);
        CommentBasicRepresentDTO GetPlainCommentById(int id);
        public IEnumerable<Comment> getCommentsForEntity(int entityId, int page = 1, int pageSize = 10);
    }
}
