using BusinessLayer.DTOs.Event;
using BusinessLayer.DTOs.Message;
using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : Controller
    {

        private readonly IMessageFacade messageFacade;

        public ChatController(IMessageFacade messageFacade)
        {
            this.messageFacade = messageFacade;
        }

        [HttpPost("messages")]
        public IActionResult SendMessage([FromBody] MessageCreateDTO messageDTO)
        {
            messageFacade.CreateMessage(messageDTO);
            return Ok();
        }
        ///conversations/{id}?page={page}&size={size}
        [HttpGet("conversations/{id}")]
        public IActionResult GetMessages(int id, int page = 1, int size = 10)
        {
            var contacts = messageFacade.GetDirectMessagesContacts(id, page, size);
            return Ok(contacts);
        }
        ///messages?user1Id={user1Id}&user2Id={user2Id}&page={page}&size={size}
        [HttpGet("messages")]
        public IActionResult GetMessagesBetween(int user1Id, int user2Id, int page = 1, int size = 10)
        {
            var messages = messageFacade.GetDirectMessagesBetween(user1Id, user2Id, page, size);
            return Ok(messages);
        }


    }
}