using EventManagement.Application.DTOs;
using EventManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<int> CreateEvent(Events eventModel);
        Task<List<Events>> GetAllEvents();
        Task<List<EventData>> GetAllPendingEvents();
        Task<List<EventData>> GetEventsByUser(int userId);
        Task<Events> GetEventDetail(Events eventModel);
        Task<List<EventTypes>> GetEventTypes();
        Task<List<Venue>> GetVenues();
        Task<int> UpdateEventById(int eventId, int eventStatus);
    }
}
