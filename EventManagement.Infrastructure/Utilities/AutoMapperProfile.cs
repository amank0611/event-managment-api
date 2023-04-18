using AutoMapper;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Entities;

namespace EventManagement.Infrastructure.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<UserDto, Users>().ReverseMap();
            CreateMap<LoginDto, Users>().ReverseMap();
            CreateMap<EventsDto, Events>().ReverseMap();
            CreateMap<EventTypesDto, EventTypes>().ReverseMap();
            CreateMap<RolesDto, Roles>().ReverseMap();
            CreateMap<VenueDto, Venue>().ReverseMap();
        }
    }
}
