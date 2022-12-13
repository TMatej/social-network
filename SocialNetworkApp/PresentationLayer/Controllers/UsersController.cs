using BusinessLayer.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using BusinessLayer.Facades.Interfaces;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserFacade userFacade;
    private readonly IProfileFacade profileFacade;

    public UsersController(IUserFacade userFacade, IProfileFacade profileFacade)
    {
        this.userFacade = userFacade;
        this.profileFacade = profileFacade;
    }

    [HttpPost]
    public void Register(UserRegisterDTO userRegisterDTO)
    {
        userFacade.Register(userRegisterDTO);
    }

    [HttpGet("{userId}/profile")]
    public IActionResult GetUserProfile(int userId)
    {
        var profile = profileFacade.GetProfileByUserId(userId);
        return Ok(profile);
    }

    [HttpPatch("{userId}/profile")]
    public IActionResult UpdateUserProfile(int userId, [FromBody] ProfileUpsertModel profile)
    {
        return Ok();
    }

    [HttpDelete("{userId}/profile")]
    public IActionResult DeleteUserProfile(int userId)
    {
        return Ok();
    }
}
