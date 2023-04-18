using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.DTOs
{
    public class EventData
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public int EventTypeId { get; set; }
        public string EventType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventHost { get; set; }
        public int EventStatus { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string ResponseMessage { get; set; }
    }
}
