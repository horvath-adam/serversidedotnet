using EventApp.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Services
{
    public interface IEventService
    {
        void Log(string message);
        List<Event> CreateExampleEvents();
    }

    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;

        public EventService(ILogger<EventService> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogInformation("EventService log: " + message);
        }

        public List<Event> CreateExampleEvents()
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
