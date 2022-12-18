using BusinessLayer.DTOs.Search;
using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        readonly IContactFacade contactFacade;

        public ContactController(IContactFacade contactFacade)
        {
            this.contactFacade = contactFacade;
        }

        //GET /contact/{id}/
        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            var users = contactFacade.GetContacts(id);
            return Ok(users);
        }

        //PUT /contact/{id}/friends?targetUserId={targetUserId}
        [HttpPut("{id}/friends")]
        public IActionResult AddContact(int id, int targetUserId)
        {
            contactFacade.AddContact(id, targetUserId);
            return Ok();
        }

        //DELETE /contact/{id}/friends?targetUserId={targetUserId}
        [HttpDelete("{id}/friends")]
        public IActionResult DeleteFriend(int id, int targetUserId)
        {
            contactFacade.RemoveContact(id, targetUserId);
            return Ok();
        }
    }
}
