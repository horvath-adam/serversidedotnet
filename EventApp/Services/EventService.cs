using EventApp.Context;
using EventApp.Models;
using EventApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Services
{
    public interface IEventService
    {
        IEnumerable<Event> GetAll();
        Event Get(int id);
        Event Create(Event newEvent);
        Event Update(int evtId, Event updatedEvent);
        IEnumerable<Event> Delete(int eventId);
        Task<Event> GetEventAsync(int evtId);
    }

    public class EventService : AbstractService, IEventService
    {
        private readonly ILogger<EventService> _logger;

        public EventService(IUnitOfWork unitOfWork, ILogger<EventService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        private void Log(string message)
        {
            _logger.LogInformation("EventService log: " + message);
        }

        public IEnumerable<Event> GetAll()
        {
            Log("GetAll");
            return UnitOfWork.GetRepository<Event>().GetAll();
        }

        public Event Get(int id)
        {
            Log("Get(" + id + ")");
            return UnitOfWork.GetRepository<Event>().GetByIdWithInclude(id, src => src.Include(evt => evt.Place));
        }

        public Event Create(Event newEvent)
        {
            Log("Create");
            newEvent.Id = 0;
            UnitOfWork.GetRepository<Event>().Create(newEvent);
            UnitOfWork.SaveChanges();
            return newEvent;
        }

        public Event Update(int evtId, Event updatedEvent)
        {
            Log("Update(" + evtId + ")");
            UnitOfWork.GetRepository<Event>().Update(evtId, updatedEvent);
            UnitOfWork.SaveChanges();
            return updatedEvent;
        }

        public IEnumerable<Event> Delete(int eventId)
        {
            Log("Delete(" + eventId + ")");
            UnitOfWork.GetRepository<Event>().Delete(eventId);
            UnitOfWork.SaveChanges();
            return GetAll();
        }

        public async Task<Event> GetEventAsync(int evtId)
        {
            // var evt = await _context.Events.Where(e => e.Id == evtId).Include(e => e.Place).FirstOrDefaultAsync();
            // return evt;

            var evt = UnitOfWork.GetRepository<Event>().GetByIdWithInclude(evtId, src => src.Include(e => e.Place));
            return evt;
        }
    }
}
