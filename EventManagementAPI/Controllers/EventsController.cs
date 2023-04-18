using EventManagement.Application.DTOs;
using EventManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EventManagementAPI.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] EventsDto eventModel)
        {
            return Ok(await _eventService.CreateEvent(eventModel));
        }

        //[Authorize]
        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            return Ok(await _eventService.GetAllEvents());
        }
        [HttpGet("GetAllPendingEvents")]
        public async Task<IActionResult> GetAllPendingEvents()
        {
            return Ok(await _eventService.GetAllPendingEvents());
        }

        [HttpGet("GetEventsByDate")]
        public async Task<IActionResult> GetEventsByDate(string date)
        {
            return Ok(await _eventService.GetEventsByDate(Convert.ToDateTime(date)));
        }

        [HttpGet]
        [Route("GetEventsByUser")]
        public async Task<IActionResult> GetEventsByUser(int userId)
        {
            return Ok(await _eventService.GetEventsByUser(userId));
        }

        [HttpDelete("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(int userId)
        {
            return Ok(await _eventService.DeleteEvent(userId));
        }

        [HttpGet("GetEventTypes")]
        public async Task<IActionResult> GetEventTypes()
        {
            var data = await _eventService.GetEventTypes();
            return Ok(data);
        }

        [HttpGet("GetVenues")]
        public async Task<IActionResult> GetVenues()
        {
            var data = await _eventService.GetVenues();
            return Ok(data);
        }
        [HttpGet]
        [Route("UpdateEvent")]
        public async Task<IActionResult> UpdateEvent(int eventId, int eventStatus)
        {
            return Ok(await _eventService.UpdateEventById(eventId, eventStatus));
        }
    }
}
