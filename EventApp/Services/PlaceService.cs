using EventApp.Context;
using EventApp.Models;
using EventApp.UnitOfWork;
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

    public class PlaceService : AbstractService, IPlaceService
    {
        private readonly ILogger<PlaceService> _logger;

        public PlaceService(IUnitOfWork unitOfWork, ILogger<PlaceService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        private void Log(string message)
        {
            _logger.LogInformation("PlaceService log: " + message);
        }

        public IEnumerable<Place> GetAll()
        {
            Log("GetAll");
            return UnitOfWork.GetRepository<Place>().GetAll();
        }

        public Place Get(int id)
        {
            Log("Get(" + id + ")");
            return UnitOfWork.GetRepository<Place>().GetByIdWithInclude(id, src => src.Include(place => place.Events));
        }

        public Place Create(Place newPlace)
        {
            Log("Create");
            newPlace.Id = 0;
            UnitOfWork.GetRepository<Place>().Create(newPlace);
            UnitOfWork.SaveChanges();
            return newPlace;
        }

        public Place Update(int placeId, Place updatedPlace)
        {
            Log("Update(" + placeId + ")");
            UnitOfWork.GetRepository<Place>().Update(placeId, updatedPlace);
            UnitOfWork.SaveChanges();
            return updatedPlace;
        }

        public IEnumerable<Place> Delete(int placeId)
        {
            Log("Delete(" + placeId + ")");
            UnitOfWork.GetRepository<Place>().Delete(placeId);
            UnitOfWork.SaveChanges();
            return GetAll();
        }






    }
}
