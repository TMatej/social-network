using BusinessLayer.DTOs.Comment;

namespace BusinessLayer.Facades.Interfaces
{
    public interface ICommentFacade
    {
        IEnumerable<CommentRepresentDTO> GetCommentsForEntity(int entityId, int page = 1, int pageSize = 10);
        void AddComment(int entityId, int userId, CommentCreateDTO commentDTO);
        void RemoveComment(int id);

    }
}
