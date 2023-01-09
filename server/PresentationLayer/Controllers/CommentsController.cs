using BusinessLayer.DTOs.Comment;
using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult AddCommentToCommentableEntity(int entityId, [FromBody] CommentCreateDTO commentCreateDTO)
        {
            var userId = HttpContext?.User.Identity?.Name;
            if (userId == null)
            {
                return Unauthorized();
            }
            commentFacade.AddComment(entityId, int.Parse(userId), commentCreateDTO);
            return Ok();
        }

        //DELETE /comments/{commentId} - odstrani komentar (kontrola kdo vytvoril)
        //TODO permission check
        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            if (!commentFacade.CheckPermission(HttpContext?.User.Identity?.Name, commentId))
            {
                return Unauthorized();
            }
            commentFacade.RemoveComment(commentId);
            return Ok();
        }
    }
}
