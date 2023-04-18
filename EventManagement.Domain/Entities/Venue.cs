using System;
using System.Collections.Generic;

namespace EventManagement.Domain.Entities
{
    public partial class Venue
    {
        public Venue()
        {
            Events = new HashSet<Events>();
        }

        public int VenueId { get; set; }
        public string VenueName { get; set; }

        public virtual ICollection<Events> Events { get; set; }
    }
}
