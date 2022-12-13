
using BusinessLayer.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using BusinessLayer.Facades.Interfaces;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class SessionsController : Controller
{
    private readonly IUserFacade userFacade;

    public SessionsController(IUserFacade userFacade)
    {
        this.userFacade = userFacade;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var authentication = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (authentication.Succeeded && authentication.Principal.Identity != null && authentication.Principal.Identity.Name != null )
        {
            var userDto = userFacade.GetUserFromCookieAuthId(int.Parse(authentication.Principal.Identity.Name));
            return Ok(userDto);
        }

        return Unauthorized();
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
