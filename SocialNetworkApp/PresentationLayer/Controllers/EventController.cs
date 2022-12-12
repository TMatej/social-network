using BusinessLayer.DTOs.Event;
using BusinessLayer.DTOs.Group;
using BusinessLayer.Facades;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public IActionResult CreateEvent(EventCreateDTO eventCreateDTO)
        {
            eventFacade.CreateEvent(eventCreateDTO);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetEvent(int eventId)
        {
            var _event = eventFacade.GetEvent(eventId);
            return _event == null ? NotFound() : Ok(_event);
        }

        [HttpPut]
        public IActionResult UpdateEvent(EventRepresentDTO eventRepresentDTO)
        {
            eventFacade.UpdateEvent(eventRepresentDTO);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteEvent(EventRepresentDTO eventRepresentDTO)
        {
            eventFacade.DeleteEvent(eventRepresentDTO);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteParticipation(EventParticipationDTO eventParticipationDTO)
        {
            var success = eventFacade.RemoveParticipant(eventParticipationDTO);
            return success ? Ok() : NotFound();
        }

        [HttpPost]
        public IActionResult AddParticipation(EventParticipationDTO eventParticipationDTO)
        {
            eventFacade.AddParticipant(eventParticipationDTO);
            return Ok();
        }
    }
}
