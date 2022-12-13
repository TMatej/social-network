using BusinessLayer.DTOs.Comment;
using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        readonly ICommentFacade commentFacade;
        public CommentsController(ICommentFacade commentFacade) {
            this.commentFacade = commentFacade;
        }
        //GET /comments?entityId={entityId}&page={page}&size={size} - vrati komentare pro commentable entitu s entityId se strankovanim
        [HttpGet]
        public IActionResult GetCommentsOfCommentableEntity(int entityId, int page, int size)
        {
            var comments = commentFacade.GetCommentsForEntity(entityId, page, size);
            var paginated = new Paginated<CommentRepresentDTO>()
            {
                Items = comments,
                Page = page,
                Size = size,
            };
            return Ok(paginated);
        }

        //POST /comments?entityId={entityId} - prida comment pod libovolnou commantable entitu s entityId
        //TODO permissions
        [HttpPost]
        public IActionResult AddCommentToCommentableEntity(int entityId, [FromBody] CommentCreateDTO commentCreateDTO)
        {
            commentFacade.AddComment(commentCreateDTO);
            return Ok();
        }

        //DELETE /comments/{commentId} - odstrani komentar (kontrola kdo vytvoril)
        //TODO permission check
        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            commentFacade.RemoveComment(commentId);
            return Ok();
        }
    }
}
