using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        //GET /comments?entityId={entityId}&page={page}&size={size} - vrati komentare pro commentable entitu s entityId se strankovanim
        [HttpGet]
        public IActionResult GetCommentsOfCommentableEntity(int entityId, int page, int size)
        {
            return Ok();
        }

        //POST /comments?entityId={entityId} - prida comment pod libovolnou commantable entitu s entityId
        [HttpPost]
        public IActionResult AddCommentToCommentableEntity(int entityId)
        {
            return Ok();
        }

        //DELETE /comments/{commentId} - odstrani komentar (kontrola kdo vytvoril)
        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            return Ok();
        }
    }
}
