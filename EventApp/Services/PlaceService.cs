using EventApp.Context;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Services
{
    public interface IPlaceService
    {
        IEnumerable<Place> GetAll();
        Place Get(int id);
        Place Create(Place newPlace);
        Place Update(int placeId, Place updatedPlace);
        IEnumerable<Place> Delete(int placeId);
    }

    public class PlaceService : IPlaceService
    {
        private readonly ILogger<PlaceService> _logger;
        private readonly EventContext _eventContext;

        public PlaceService(ILogger<PlaceService> logger, EventContext eventContext)
        {
            _eventContext = eventContext;
            _logger = logger;
        }

        private void Log(string message)
        {
            _logger.LogInformation("PlaceService log: " + message);
        }

        public IEnumerable<Place> GetAll()
        {
            Log("GetAll");
            return _eventContext.Places;
        }

        public Place Get(int id)
        {
            Log("Get(" + id + ")");
            return _eventContext.Places.Include(place => place.Events).FirstOrDefault(e => e.Id == id);
        }

        public Place Create(Place newPlace)
        {
            Log("Create");
            newPlace.Id = 0;
            _eventContext.Places.Add(newPlace);
            _eventContext.SaveChanges();
            return newPlace;
        }

        public Place Update(int placeId, Place updatedPlace)
        {
            Log("Update(" + placeId + ")");
            var place = _eventContext.Places.FirstOrDefault(e => e.Id == placeId);
            if (place != null)
            {
                place = updatedPlace;
                place.Id = placeId;
                _eventContext.Places.Update(place);
                _eventContext.SaveChanges();
            }
            return place;
        }

        public IEnumerable<Place> Delete(int placeId)
        {
            Log("Delete(" + placeId + ")");
            var place = Get(placeId);
            _eventContext.Places.Remove(place);
            _eventContext.SaveChanges();
            return _eventContext.Places;
        }

        

        

        
    }
}
