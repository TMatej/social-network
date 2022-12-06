using BusinessLayer.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
      private readonly IUserFacade userFacade;

      public UserController(IUserFacade userFacade)
      {
          this.userFacade = userFacade;
      }

      [HttpPost]
      public async Task Register(UserRegisterDTO userRegisterDTO)
      {
        userFacade.Register(userRegisterDTO);
      }
    }
}
