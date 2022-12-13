﻿using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostFacade _postFacade;
        public PostsController(IHttpContextAccessor httpContextAccessor, IPostFacade postFacade)
        {
            _httpContextAccessor = httpContextAccessor;
            _postFacade = postFacade;
        }

        //PUT /posts/{postId}/like a taky DELETE /posts/{postId}/like - like a dislike postu (u komentaru asi nebudeme resit)
        [HttpPut("{postId}/like")]
        public IActionResult GiveLikeToPost(int postId)
        {
            var username = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            if (username == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(username);
            _postFacade.LikePost(postId, userId);
            return Ok();
        }

        [HttpDelete("{postId}/like")]
        public IActionResult DeleteLikeFromPost(int postId)
        {
            var username = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            if (username == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(username);
            _postFacade.UnlikePost(postId, userId);
            return Ok();
        }

        //DELETE /posts/{postId} - odstrani post (kontrola kdo vytvoril) 
        //TODO check permissions
        [HttpDelete("{postId}")]
        public IActionResult DeletePost(int postId)
        {
            _postFacade.DeletePost(postId);
            return Ok();
        }

    }
}
