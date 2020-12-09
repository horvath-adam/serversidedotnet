using System.Collections.Generic;
using EventApp.Models;
using EventApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Gets all Event.
        /// </summary>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/events/getall
        [HttpGet]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult<IEnumerable<Event>> GetAll()
        {
            return Ok(_eventService.GetAll());
        }

        /// <summary>
        /// Gets a specific Event.
        /// </summary>
        /// <param name="evtId">The unique ID of the Event</param>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/events/get/1
        [HttpGet("{evtId}")]
        [Authorize(Roles = "Administrator, User")]
        [Authorize(Policy = "AdultsOnly")]
        public ActionResult<Event> Get(int eventId)
        {
            return Ok(_eventService.Get(eventId));
        }

        /// <summary>
        /// Creates a new Event.
        /// </summary>
        /// <param name="evtDto">New Event data</param>
        /// <response code="201">Successful create</response>
        /// <response code="500">Server error</response>
        // POST api/events/create
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([FromBody] Event newEvent)
        {
            var evt = _eventService.Create(newEvent);
            return Created($"{evt.Id}", evt);
        }

        /// <summary>
        /// Updates a specific Event.
        /// </summary>
        /// <param name="evtId">The unique ID of the Event</param>
        /// <param name="evtDto">Updated Event data</param>
        /// <response code="200">Successful update</response>
        /// <response code="500">Server error</response>
        // PUT api/events/update/1
        [HttpPut("{evtId}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Update(int evtId, [FromBody] Event updatedEvent)
        {
            return Ok(_eventService.Update(evtId, updatedEvent));
        }

        /// <summary>
        /// Deletes a specific Event.
        /// </summary>
        /// <param name="evtId">The unique ID of the Event</param>
        /// <response code="200">Successful delete</response>
        /// <response code="500">Server error</response>
        // DELETE api/events/delete/1
        [HttpDelete("{evtId}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int evtId)
        {
            return Ok(_eventService.Delete(evtId));
        }
    }
}
