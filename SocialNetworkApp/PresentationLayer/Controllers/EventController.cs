using BusinessLayer.DTOs.Event;
using BusinessLayer.DTOs.Group;
using BusinessLayer.Facades;
using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {

        private readonly IEventFacade eventFacade;

        public EventController(IEventFacade eventFacade)
        {
            this.eventFacade = eventFacade;
        }

        [HttpPost("/event")]
        public IActionResult CreateEvent(EventCreateDTO eventCreateDTO)
        {
            eventFacade.CreateEvent(eventCreateDTO);
            return Ok();
        }

        [HttpGet("/event/{eventId}")]
        public IActionResult GetEvent(int eventId)
        {
            var _event = eventFacade.GetEvent(eventId);
            return _event == null ? NotFound() : Ok(_event);
        }

        [HttpPut("/event")]
        public IActionResult UpdateEvent([FromBody]EventRepresentDTO eventRepresentDTO)
        {
            if (!eventFacade.CheckPermission(HttpContext.User.Identity.Name, eventRepresentDTO.Id))
            {
                return Unauthorized();
            }
            eventFacade.UpdateEvent(eventRepresentDTO);
            return Ok();
        }

        [HttpDelete("/event")]
        public IActionResult DeleteEvent([FromBody]EventRepresentDTO eventRepresentDTO)
        {
            if (!eventFacade.CheckPermission(HttpContext.User.Identity.Name, eventRepresentDTO.Id))
            {
                return Unauthorized();
            }
            eventFacade.DeleteEvent(eventRepresentDTO);
            return Ok();
        }

        [HttpDelete("/participation")]
        public IActionResult DeleteParticipation([FromBody]EventParticipationDTO eventParticipationDTO)
        {
            if (!(eventFacade.CheckPermission(HttpContext.User.Identity.Name, eventParticipationDTO.EventId) || int.Parse(HttpContext.User.Identity.Name) == eventParticipationDTO.UserId))
            {
                return Unauthorized();
            }
            var success = eventFacade.RemoveParticipant(eventParticipationDTO);
            return success ? Ok() : NotFound();
        }

        [HttpPost("/participation")]
        public IActionResult AddParticipation([FromBody]EventParticipationDTO eventParticipationDTO)
        {
            eventFacade.AddParticipant(eventParticipationDTO);
            return Ok();
        }
    }
}
