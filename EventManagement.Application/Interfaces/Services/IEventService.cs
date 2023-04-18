using EventManagement.Application.DTOs;
using EventManagement.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces.Services
{
    public interface IEventService
    {
        Task<RepositoryResponse> CreateEvent(EventsDto eventModel);
        Task<List<EventsDto>> GetAllEvents();
        Task<RepositoryResponse> GetAllPendingEvents();
        Task<RepositoryResponse> GetEventsByUser(int userId);
        Task<IList<EventsDto>> GetEventsByDate(DateTime date);
        Task<int> DeleteEvent(int userId);
        Task<List<EventTypesDto>> GetEventTypes();
        Task<List<VenueDto>> GetVenues();
        Task<RepositoryResponse> UpdateEventById(int eventId, int eventStatus);
    }
}
