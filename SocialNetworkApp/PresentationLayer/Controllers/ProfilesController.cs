using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfilesController : ControllerBase
{
    public ProfilesController()
    {
    }

    // GET /profiles/{profileId}/posts?page={page}&size={size} - vrati posty pro danej profil se strankovanim
    [HttpGet("{profileId}/posts")]
    public IActionResult GetProfilePosts(int profileId, int page, int size)
    {
        return Ok();
    }

    // POST /profiles/{profileId}/posts - prida post pod profil, melo by se kontrolovat jestli je uzivatel v pratelich nebo profil vlastni
    [HttpPost("{profileId}/posts")]
    public IActionResult AddProfilePost(int profileId, [FromBody] PostUpsertModel post)
    {
        return Ok();
    }
}
