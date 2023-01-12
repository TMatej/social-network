using Microsoft.AspNetCore.Mvc;
using BusinessLayer.DTOs.Group;
using BusinessLayer.Facades.Interfaces;
using PresentationLayer.Models;
using BusinessLayer.DTOs.Post;
using Microsoft.AspNetCore.Authorization;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly IGroupFacade groupFacade;
        private readonly IPostFacade postFacade;

        public GroupsController(IGroupFacade groupFacade, IPostFacade postFacade)
        {
            this.groupFacade = groupFacade;
            this.postFacade = postFacade;
        }
        
        [HttpPost]
        public IActionResult CreateGroup(GroupCreateDTO groupCreateDTO)
        {
            groupFacade.CreateGroup(groupCreateDTO, int.Parse(HttpContext.User.Identity.Name));
            return Ok();
        }
        
        [HttpGet("{groupId}")]
        public IActionResult GetGroup(int groupId)
        {
            var group = groupFacade.GetGroup(groupId);
            return group == null ? NotFound() : Ok(group);
        }

        [HttpGet("{groupId}/posts")]
        public IActionResult GetGroupPosts(int groupId, int page = 1, int size = 10)
        {
            var posts = postFacade.GetPostForPostable(groupId, page, size);
            var paginatedPosts = new Paginated<PostRepresentDTO>()
            {
                Items = posts,
                Page = page,
                Size = size,
            };
            return Ok(paginatedPosts);
        }

        [HttpPost("{groupId}/posts")]
        public IActionResult AddGroupPost(int groupId, [FromBody] PostCreateDTO post)
        {
            postFacade.CreatePost(post);
            return Ok();
        }
        
        [HttpPut]
        public IActionResult UpdateGroup(GroupRepresentDTO groupRepresentDTO)
        {
            if (!groupFacade.CheckPermission(HttpContext.User.Identity.Name, groupRepresentDTO.Id))
            {
                return Unauthorized();
            }
            groupFacade.UpdateGroup(groupRepresentDTO);
            return Ok();
        }
        
        [HttpDelete("{groupId}")]
        public IActionResult DeleteGroup(int groupId)
        {
            if (!groupFacade.CheckPermission(HttpContext.User.Identity.Name, groupId))
            {
                return Unauthorized();
            }
            groupFacade.DeleteGroup(groupId);
            return Ok();
        }

        [HttpDelete("{groupId}/membership")]
        public IActionResult DeleteMembership(int groupId)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            var success = groupFacade.RemoveFromGroup(groupId, userId);
            return success ? Ok() : NotFound();
        }

        [HttpPut("{groupId}/membership")]
        public IActionResult AddMembership(int groupId)
        {
            var userId = int.Parse(HttpContext.User.Identity.Name);
            groupFacade.AddToGroup(groupId, userId);
            return Ok();
        }
    }
}
