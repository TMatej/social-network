using BusinessLayer.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.DTOs.Profile;
using Microsoft.AspNetCore.Authorization;

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

    ///users?page={page}&size={size}
    [HttpGet]
    [Authorize]
    public IActionResult GetAllUsers(int page = 1, int size = 10)
    {
        var users = userFacade.GetAllUsersPaginated(page, size);
        return Ok(users);
    }

    [HttpDelete("{userId}")]
    [Authorize]
    public IActionResult DeleteUser(int userId)
    {
        if (userId != int.Parse(HttpContext.User.Identity.Name))
        {
            return Unauthorized();
        }
        userFacade.DeleteUser(userId);
        return Ok();
    }

    [HttpGet("{userId}/profile")]
    public IActionResult GetUserProfile(int userId)
    {
        try
        {
            var profile = profileFacade.GetProfileByUserId(userId);
            return Ok(profile);
        } catch (Exception ex)
        {
            return NotFound("Resource not found!");
        }
    }

    [HttpGet("{userId}/groups")]
    [Authorize]
    public IActionResult GetUserGroups(int userId)
    {
        if (userId != int.Parse(HttpContext.User.Identity.Name))
        {
            return Unauthorized();
        }
        var groups = userFacade.GetGroupsForUser(userId);
        return Ok(groups);
    }

    [HttpPut("{userId}/avatar")]
    [Authorize]
    public IActionResult UpdateAvatar(int userId, [FromForm] UpdateUserAvatarDTO updateUserAvatarDTO)
    {
        if (userId != int.Parse(HttpContext.User.Identity.Name))
        {
            return Unauthorized();
        }
        userFacade.UpdateUserAvatar(userId, updateUserAvatarDTO.avatar);
        return Ok();
    }

    [HttpPatch("{userId}/profile")]
    [Authorize]
    public IActionResult UpdateUserProfile(int userId, [FromBody] ProfileUpdateDTO profileUpdateDTO)
    {
        profileFacade.UpdateProfile(int.Parse(HttpContext.User.Identity.Name), profileUpdateDTO);
        return Ok();
    }

    [HttpDelete("{userId}/profile")]
    public IActionResult DeleteUserProfile(int userId)
    {
        return Ok();
    }
}
