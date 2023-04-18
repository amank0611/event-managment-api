using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Application.Helpers;
using EventManagement.Application.Interfaces.Repositories;
using EventManagement.Application.Interfaces.Services;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EventManagement.Infrastructure.Implementations.Services
{
    public class EventService : IEventService
    {
        private readonly IMapper _autoMapper;
        private readonly IEventRepository _eventRepository;
        public EventService(IMapper autoMapper, IEventRepository eventRepository)
        {
            _autoMapper = autoMapper;
            _eventRepository = eventRepository;
        }

        public async Task<RepositoryResponse> CreateEvent(EventsDto eventModel)
        {
            var mappedModel = _autoMapper.Map<Events>(eventModel);
            var eventDetails = await _eventRepository.GetEventDetail(mappedModel);
            if (eventDetails == null)
            {
                eventModel.CreatedOn = DateTime.Now;
                eventModel.IsActive = true;
                eventModel.IsDeleted = false;
                var obj = _autoMapper.Map<Events>(eventModel);
                eventModel.EventId = await _eventRepository.CreateEvent(obj);
                return new RepositoryResponse(new
                {
                    Response = eventModel,
                    Status = RepositoryResponseError.CreateSuccess
                });
            }
            else
            {
                return new RepositoryResponse(new
                {
                    Status = RepositoryResponseError.AlreadyExist
                });

            }
        }

        public Task<int> DeleteEvent(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EventsDto>> GetAllEvents()
        {
            var data = await _eventRepository.GetAllEvents();
            return _autoMapper.Map<List<EventsDto>>(data);
        }

        public async Task<RepositoryResponse> GetAllPendingEvents()
        {
            var result = await _eventRepository.GetAllPendingEvents();
            if (result.Count > 0)
            {
                return new RepositoryResponse(new { Succeeded = true, Result = result });
            }
            else
            {
                return new RepositoryResponse(new { Succeeded = false });
            }
        }

        public Task<IList<EventsDto>> GetEventsByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<RepositoryResponse> GetEventsByUser(int userId)
        {
            var result = await _eventRepository.GetEventsByUser(userId);
            if (result.Count > 0)
            {
                return new RepositoryResponse(new { Succeeded = true, Result = result });
            }
            else
            {
                return new RepositoryResponse(new { Succeeded = false });
            }
        }

        public async Task<List<EventTypesDto>> GetEventTypes()
        {
            var data = await _eventRepository.GetEventTypes();
            return _autoMapper.Map<List<EventTypesDto>>(data);
        }

        public async Task<List<VenueDto>> GetVenues()
        {
            var data = await _eventRepository.GetVenues();
            return _autoMapper.Map<List<VenueDto>>(data);
        }

        public async Task<RepositoryResponse> UpdateEventById(int eventId, int eventStatus)
        {
            var result = await _eventRepository.UpdateEventById(eventId, eventStatus);
            if (result > 0)
            {
                return new RepositoryResponse(new { Succeeded = true });
            }
            else
            {
                return new RepositoryResponse(new { Succeeded = false });
            }
        }
    }
}
