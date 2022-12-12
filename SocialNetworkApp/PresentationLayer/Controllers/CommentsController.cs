using BusinessLayer.Contracts;
using BusinessLayer.DTOs.Comment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        readonly ICommentService commentService;
        public CommentsController(ICommentService commentService) {
            this.commentService = commentService;
        }
        //GET /comments?entityId={entityId}&page={page}&size={size} - vrati komentare pro commentable entitu s entityId se strankovanim
        [HttpGet("?entityId={entityId}&page={page}&size={size}")]
        public IActionResult GetCommentsOfCommentableEntity(int entityId, int page, int size)
        {
            var comments = commentService.getCommentsForEntity(entityId, page, size);
            return Ok(comments);
        }

        //POST /comments?entityId={entityId} - prida comment pod libovolnou commantable entitu s entityId
        //TODO permissions
        [HttpPost]
        public IActionResult AddCommentToCommentableEntity(int entityId, [FromBody] CommentCreateDTO commentCreateDTO)
        {
            commentService.AddComment(commentCreateDTO);
            return Ok();
        }

        //DELETE /comments/{commentId} - odstrani komentar (kontrola kdo vytvoril)
        //TODO permission check
        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            commentService.Delete(commentId);
            return Ok();
        }
    }
}
