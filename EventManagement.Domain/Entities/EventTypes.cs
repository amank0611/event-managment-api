using System;
using System.Collections.Generic;

namespace EventManagement.Domain.Entities
{
    public partial class EventTypes
    {
        public EventTypes()
        {
            Events = new HashSet<Events>();
        }

        public int Id { get; set; }
        public string EventType { get; set; }

        public virtual ICollection<Events> Events { get; set; }
    }
}
