using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private List<Event> _events;

        public EventsController()
        {
            _events = CreateExampleEvents();
        }

        /// <summary>
        /// Gets all Event.
        /// </summary>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/events/getall
        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetAll()
        {
            return Ok(_events);
        }

        /// <summary>
        /// Gets a specific Event.
        /// </summary>
        /// <param name="evtId">The unique ID of the Event</param>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/events/get/1
        [HttpGet("{evtId}")]
        public ActionResult<Event> Get(int evtId)
        {
            var evt = _events.FirstOrDefault(e => e.Id == evtId);
            if(evt != null)
            {
                return Ok(evt);
            }
            return StatusCode(500);
        }

        /// <summary>
        /// Creates a new Event.
        /// </summary>
        /// <param name="evtDto">New Event data</param>
        /// <response code="201">Successful create</response>
        /// <response code="500">Server error</response>
        // POST api/events/create
        [HttpPost]
        public IActionResult Create([FromBody] Event newEvent)
        {
            _events.Add(newEvent);
            return Created($"{newEvent.Id}", newEvent);
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
        public IActionResult Update(int evtId, [FromBody] Event updatedEvent)
        {
            var evt = _events.FirstOrDefault(e => e.Id == evtId);
            if(evt != null)
            {
                evt = updatedEvent;
                evt.Id = evtId;
                return Ok(_events);
            }
            return StatusCode(500);
        }

        /// <summary>
        /// Deletes a specific Event.
        /// </summary>
        /// <param name="evtId">The unique ID of the Event</param>
        /// <response code="200">Successful delete</response>
        /// <response code="500">Server error</response>
        // DELETE api/events/delete/1
        [HttpDelete("{evtId}")]
        public IActionResult Delete(int evtId)
        {
            _events.RemoveAll(e => e.Id == evtId);
            return Ok(_events);
        }

        private List<Event> CreateExampleEvents()
        {
            var events = new List<Event>();

            events.Add(new Event
            {
                Id = 1,
                Description = "Test event 1",
                Name = "Test event 1",
                Place = "Veszprém",
                Start = new DateTime(2020, 09, 10),
                End = new DateTime(2020, 09, 11)
            });
            events.Add(new Event
            {
                Id = 2,
                Description = "Test event 2",
                Name = "Test event 2",
                Place = "Budapest",
                Start = new DateTime(2020, 09, 21),
                End = new DateTime(2020, 09, 23)
            });
            events.Add(new Event
            {
                Id = 3,
                Description = "Test event 3",
                Name = "Test event 3",
                Place = "Miskolc",
                Start = new DateTime(2020, 10, 01),
                End = new DateTime(2020, 10, 31)
            });
            events.Add(new Event
            {
                Id = 4,
                Description = "Test event 4",
                Name = "Test event 4",
                Place = "Szeged",
                Start = new DateTime(2020, 10, 04),
                End = new DateTime(2020, 10, 11)
            });
            events.Add(new Event
            {
                Id = 5,
                Description = "Test event 5",
                Name = "Test event 5",
                Place = "Debrecen",
                Start = new DateTime(2020, 11, 03),
                End = new DateTime(2020, 12, 14)
            });

            return events;
        }
    }
}
