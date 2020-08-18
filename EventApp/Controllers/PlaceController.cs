using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;

        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        /// <summary>
        /// Gets all Place.
        /// </summary>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/place/getall
        [HttpGet]
        public ActionResult<IEnumerable<Place>> GetAll()
        {
            return Ok(_placeService.GetAll());
        }

        /// <summary>
        /// Gets a specific Event.
        /// </summary>
        /// <param name="evtId">The unique ID of the Event</param>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/place/get/1
        [HttpGet("{evtId}")]
        public ActionResult<Place> Get(int placeId)
        {
            return Ok(_placeService.Get(placeId));
        }

        /// <summary>
        /// Creates a new Place.
        /// </summary>
        /// <param name="evtDto">New Place data</param>
        /// <response code="201">Successful create</response>
        /// <response code="500">Server error</response>
        // POST api/place/create
        [HttpPost]
        public IActionResult Create([FromBody] Place newPlace)
        {
            var evt = _placeService.Create(newPlace);
            return Created($"{evt.Id}", evt);
        }

        /// <summary>
        /// Updates a specific Place.
        /// </summary>
        /// <param name="evtId">The unique ID of the Place</param>
        /// <param name="evtDto">Updated Place data</param>
        /// <response code="200">Successful update</response>
        /// <response code="500">Server error</response>
        // PUT api/place/update/1
        [HttpPut("{placeId}")]
        public IActionResult Update(int placeId, [FromBody] Place updatedPlace)
        {
            return Ok(_placeService.Update(placeId, updatedPlace));
        }

        /// <summary>
        /// Deletes a specific Place.
        /// </summary>
        /// <param name="evtId">The unique ID of the Place</param>
        /// <response code="200">Successful delete</response>
        /// <response code="500">Server error</response>
        // DELETE api/place/delete/1
        [HttpDelete("{placeId}")]
        public IActionResult Delete(int placeId)
        {
            return Ok(_placeService.Delete(placeId));
        }
    }
}
