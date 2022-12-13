using BusinessLayer.DTOs.Post;
using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfilesController : ControllerBase
{
    IPostFacade postFacade;
    IProfileFacade profileFacade;
    public ProfilesController(IPostFacade postFacade, IProfileFacade profileFacade)
    {
       this.postFacade = postFacade;
       this.profileFacade = profileFacade;
    }

    // GET /profiles/{profileId}/posts?page={page}&size={size} - vrati posty pro danej profil se strankovanim
    [HttpGet("{profileId}/posts")]
    public IActionResult GetProfilePosts(int profileId, int page, int size)
    {
        var posts = postFacade.GetPostForPostable(profileId, page, size);
        var paginatedPosts = new Paginated<PostRepresentDTO>()
        {
            Items = posts,
            Page = page,
            Size = size,
        };
        return Ok(paginatedPosts);
    }

    // POST /profiles/{profileId}/posts - prida post pod profil, melo by se kontrolovat jestli je uzivatel v pratelich nebo profil vlastni
    [HttpPost("{profileId}/posts")]
    public IActionResult AddProfilePost(int profileId, [FromBody] PostCreateDTO post)
    {
        postFacade.CreatePost(post);
        return Ok();
    }
}
