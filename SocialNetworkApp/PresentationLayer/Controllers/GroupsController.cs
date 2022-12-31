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
        [Authorize]
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

        [Authorize]
        [HttpPost("{groupId}/posts")]
        public IActionResult AddGroupPost(int groupId, [FromBody] PostCreateDTO post)
        {
            postFacade.CreatePost(post);
            return Ok();
        }
        
        [HttpPut]
        public IActionResult UpdateGroup(GroupRepresentDTO groupRepresentDTO)
        {
            groupFacade.UpdateGroup(groupRepresentDTO);
            return Ok();
        }
        
        [HttpDelete("{groupId}")]
        public IActionResult DeleteGroup(int groupId)
        {
            groupFacade.DeleteGroup(groupId);
            return Ok();
        }

        [HttpDelete("/membership")]
        public IActionResult DeleteMembership(GroupMembershipDTO groupMembershipDTO)
        {
            var success = groupFacade.RemoveFromGroup(groupMembershipDTO);
            return success ? Ok() : NotFound();
        }

        [HttpPost("/membership")]
        public IActionResult AddMembership(GroupMembershipDTO groupMembershipDTO)
        {
            groupFacade.AddToGroup(groupMembershipDTO);
            return Ok();
        }
    }
}
