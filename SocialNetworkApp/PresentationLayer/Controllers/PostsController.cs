using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        public PostsController()
        {
        }

        //PUT /posts/{postId}/like a taky DELETE /posts/{postId}/like - like a dislike postu (u komentaru asi nebudeme resit)
        [HttpPut("{postId}/like")]
        public IActionResult GiveLikeToPost(int postId)
        {
            return Ok();
        }

        [HttpDelete("{postId}/like")]
        public IActionResult DeleteLikeFromPost(int postId)
        {
            return Ok();
        }

        //DELETE /posts/{postId} - odstrani post (kontrola kdo vytvoril) 
        [HttpDelete("{postId}")]
        public IActionResult DeletePost(int postId)
        {
            return Ok();
        }

    }
}
