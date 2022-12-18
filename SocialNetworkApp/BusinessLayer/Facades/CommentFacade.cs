using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Comment;
using BusinessLayer.Facades.Interfaces;
using DataAccessLayer.Entity;


namespace BusinessLayer.Facades
{
    public class CommentFacade : ICommentFacade
    {
        ICommentService commentService;
        IMapper mapper;

        public CommentFacade(ICommentService commentService, IMapper mapper)
        {
            this.commentService = commentService;
            this.mapper = mapper;
        }

        public void AddComment(int entityId, int userId, CommentCreateDTO commentDTO)
        {
            commentService.Insert(new Comment() {
                UserId = userId,
                CommentableId = entityId,
                Content = commentDTO.Content,
            });
        }

        public IEnumerable<CommentRepresentDTO> GetCommentsForEntity(int entityId, int page = 1, int pageSize = 10)
        {
            var services = commentService.getCommentsForEntity(entityId, page, pageSize);
            return services.Select(e => mapper.Map<CommentRepresentDTO>(e));
        }

        public void RemoveComment(int id)
        {
            commentService.Delete(id);
        }
    }
}
