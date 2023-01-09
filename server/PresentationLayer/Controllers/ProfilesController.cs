using BusinessLayer.DTOs.Gallery;
using BusinessLayer.DTOs.Post;
using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
    [Authorize]
    [HttpPost("{profileId}/posts")]
    public IActionResult AddProfilePost(int profileId, [FromBody] PostCreateDTO post)
    {
        postFacade.CreatePost(post);
        return Ok();
    }

    [Authorize]
    [HttpGet("{profileId}/galleries")]
    public IActionResult GetGalleries(int profileId)
    {
        var galleries = profileFacade.GetGalleriesByProfileId(profileId);
        return Ok(galleries);
    }

    [Authorize]
    [HttpPost("{profileId}/galleries")]
    public IActionResult CreateGallery(int profileId, [FromBody] GalleryCreateDTO galleryCreateDTO)
    {
        var userId = int.Parse(HttpContext.User.Identity.Name);
        profileFacade.CreateGallery(userId, profileId, galleryCreateDTO);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{profileId}/galleries/{galleryId}")]
    public IActionResult DeleteGallery(int profileId, int galleryId)
    {
        var userId = int.Parse(HttpContext.User.Identity.Name);
        profileFacade.DeleteGallery(userId, profileId, galleryId);
        return Ok();
    }
}
