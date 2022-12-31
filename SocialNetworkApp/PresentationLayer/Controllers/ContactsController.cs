using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        readonly IContactFacade contactFacade;

        public ContactsController(IContactFacade contactFacade)
        {
            this.contactFacade = contactFacade;
        }

        //GET /contacts/{id}/
        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            var users = contactFacade.GetContacts(id);
            return Ok(users);
        }

        //PUT /contacts/{id}/friends?targetUserId={targetUserId}
        [HttpPut("{id}/friends")]
        public IActionResult AddContact(int id, int targetUserId)
        {
            contactFacade.AddContact(id, targetUserId);
            return Ok();
        }

        //DELETE /contacts/{id}/friends?targetUserId={targetUserId}
        [HttpDelete("{id}/friends")]
        public IActionResult DeleteFriend(int id, int targetUserId)
        {
            contactFacade.RemoveContact(id, targetUserId);
            return Ok();
        }
    }
}
