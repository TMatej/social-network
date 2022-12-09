using BusinessLayer.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : Controller
{
    private readonly IUserFacade userFacade;

    public UsersController(IUserFacade userFacade)
    {
        this.userFacade = userFacade;
    }

    [HttpPost]
    public void Register(UserRegisterDTO userRegisterDTO)
    {
        userFacade.Register(userRegisterDTO);
    }
}
