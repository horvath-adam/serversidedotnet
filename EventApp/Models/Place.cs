using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Models
{
    public class Place : AbstractEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public IList<Event> Events { get; set; }
    }
}
