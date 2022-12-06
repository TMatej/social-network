
using BusinessLayer.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using BusinessLayer.Facades;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class SessionController : Controller
{
    private readonly IUserFacade userFacade;

    public SessionController(IUserFacade userFacade)
    {
        this.userFacade = userFacade;
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
    {

        var userDto = userFacade.Login(userLoginDTO);
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, userDto.Id.ToString()),
    };

        foreach (var role in userDto.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return Ok(userDto);
    }

    [HttpDelete]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
