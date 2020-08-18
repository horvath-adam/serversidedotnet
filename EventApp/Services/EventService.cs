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
    public interface IEventService
    {
        IEnumerable<Event> GetAll();
        Event Get(int id);
        Event Create(Event newEvent);
        Event Update(int evtId, Event updatedEvent);
        IEnumerable<Event> Delete(int eventId);
    }

    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;
        private readonly EventContext _eventContext;

        public EventService(ILogger<EventService> logger, EventContext eventContext)
        {
            _logger = logger;
            _eventContext = eventContext;
        }

        private void Log(string message)
        {
            _logger.LogInformation("EventService log: " + message);
        }

        public IEnumerable<Event> GetAll()
        {
            Log("GetAll");
            return _eventContext.Events;
        }

        public Event Get(int id)
        {
            Log("Get(" + id + ")");
            return _eventContext.Events.Include(evt => evt.Place).FirstOrDefault(e => e.Id == id);
        }

        public Event Create(Event newEvent)
        {
            Log("Create");
            newEvent.Id = 0;
            _eventContext.Events.Add(newEvent);
            _eventContext.SaveChanges();
            return newEvent;
        }

        public Event Update(int evtId, Event updatedEvent)
        {
            Log("Update(" + evtId + ")");
            var evt = _eventContext.Events.FirstOrDefault(e => e.Id == evtId);
            if (evt != null)
            {
                evt = updatedEvent;
                evt.Id = evtId;
                _eventContext.Events.Update(evt);
                _eventContext.SaveChanges();
            }
            return evt;
        }

        public IEnumerable<Event> Delete(int eventId)
        {
            Log("Delete(" + eventId + ")");
            var evt = Get(eventId);
            _eventContext.Events.Remove(evt);
            _eventContext.SaveChanges();
            return _eventContext.Events;
        }
    }
}
