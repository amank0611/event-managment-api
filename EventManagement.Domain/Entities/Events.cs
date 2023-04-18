using System;
using System.Collections.Generic;

namespace EventManagement.Domain.Entities
{
    public partial class Events
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public int? VenueId { get; set; }
        public int? EventTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UserId { get; set; }
        public string EventHost { get; set; }
        public int? EventStatus { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual EventTypes EventType { get; set; }
        public virtual Users User { get; set; }
        public virtual Venue Venue { get; set; }
    }
}
