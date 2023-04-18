using EventManagement.Application.DTOs;
using EventManagement.Application.Interfaces.Repositories;
using EventManagement.Application.Utilities;
using EventManagement.Domain.Entities;
using EventManagement.Infrastructure.Persistence.DataContext;
using EventManagement.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Implementations.Repositories
{
    public class EventRepository : IEventRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public EventRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> CreateEvent(Events eventModel)
        {
            await _applicationDbContext.Events.AddAsync(eventModel);
            await _applicationDbContext.SaveChangesAsync();
            return eventModel.EventId;
        }

        public async Task<List<Events>> GetAllEvents()
        {
            return await _applicationDbContext.Events.ToListAsync();
        }

        public async Task<List<EventData>> GetAllPendingEvents()
        {
            var result = await (from e in _applicationDbContext.Events
                                join v in _applicationDbContext.Venue on e.VenueId equals v.VenueId
                                where e.EventStatus == 0
                                select new EventData()
                                {
                                    EventId = e.EventId,
                                    EventName = e.EventName,
                                    EventDescription = e.EventDescription,
                                    VenueId = v.VenueId,
                                    VenueName = v.VenueName,
                                    StartDate = Convert.ToDateTime(e.StartDate).Date,
                                    EndDate = Convert.ToDateTime(e.EndDate).Date,
                                    EventHost = e.EventHost,
                                    EventStatus = (int)e.EventStatus
                                }).ToListAsync();

            return result;
        }

        public async Task<Events> GetEventDetail(Events eventModel)
        {
            Events events = new Events();
            var eventDetail = await _applicationDbContext.Events.Where(p => p.VenueId == eventModel.VenueId).ToListAsync();
            if (eventDetail.Count > 0)
            {
                DateTime startDateToCheck = Convert.ToDateTime(eventModel.StartDate).Date;
                DateTime endDateToCheck = Convert.ToDateTime(eventModel.EndDate).Date;
                foreach (var item in eventDetail)
                {
                    DateTime startDate = Convert.ToDateTime(item.StartDate).Date;
                    DateTime endDate = Convert.ToDateTime(item.EndDate).Date;

                    var validStartDate = DateTimeExtensions.InRange(startDateToCheck, startDate, endDate);
                    var validEndDate = DateTimeExtensions.InRange(endDateToCheck, startDate, endDate);

                    if (validStartDate && !validEndDate || !validStartDate && validEndDate || validStartDate && validEndDate)
                    {
                        events = item;
                        break;
                    }
                    events = null;
                    continue;
                }
            }
            else
            {
                return null;
            }
            return events;
        }

        public async Task<List<EventData>> GetEventsByUser(int userId)
        {
            EventData eventData = new EventData();
            var roleId = _applicationDbContext.Users.FirstOrDefault(p => p.UserId == userId).RoleId;
            if (roleId == (int)UserRole.Organiser)
            {
                return await (from e in _applicationDbContext.Events
                              join v in _applicationDbContext.Venue on e.VenueId equals v.VenueId
                              select new EventData()
                              {
                                  EventId = e.EventId,
                                  EventName = e.EventName,
                                  EventDescription = e.EventDescription,
                                  VenueId = v.VenueId,
                                  VenueName = v.VenueName,
                                  StartDate = Convert.ToDateTime(e.StartDate).Date,
                                  EndDate = Convert.ToDateTime(e.EndDate).Date,
                                  EventHost = e.EventHost,
                                  EventStatus = (int)e.EventStatus
                              }).ToListAsync();
            }
            else
            {
                return await (from e in _applicationDbContext.Events
                              join v in _applicationDbContext.Venue on e.VenueId equals v.VenueId
                              where e.UserId == userId
                              select new EventData()
                              {
                                  EventId = e.EventId,
                                  EventName = e.EventName,
                                  EventDescription = e.EventDescription,
                                  VenueId = v.VenueId,
                                  VenueName = v.VenueName,
                                  StartDate = Convert.ToDateTime(e.StartDate).Date,
                                  EndDate = Convert.ToDateTime(e.EndDate).Date,
                                  EventHost = e.EventHost,
                                  EventStatus = (int)e.EventStatus
                              }).ToListAsync();
            }
        }

        public async Task<List<EventTypes>> GetEventTypes()
        {
            return await _applicationDbContext.EventTypes.ToListAsync();
        }

        public async Task<List<Venue>> GetVenues()
        {
            return await _applicationDbContext.Venue.ToListAsync();
        }

        public async Task<int> UpdateEventById(int eventId, int eventStatus)
        {
            int updateId = 0;
            var evnt = await _applicationDbContext.Events.FirstOrDefaultAsync(p => p.EventId == eventId);
            if (evnt != null)
            {
                evnt.EventStatus = eventStatus;
                _applicationDbContext.Entry(evnt).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();
                return updateId = evnt.EventId;
            }
            return updateId;
        }
    }
}
