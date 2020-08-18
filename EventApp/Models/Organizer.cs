using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Models
{
    public class Organizer : AbstractEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<EventStaff> Events { get; set; }
    }
}
