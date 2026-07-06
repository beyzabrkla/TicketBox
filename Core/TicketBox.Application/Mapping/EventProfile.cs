using AutoMapper;
using TicketBox.Application.Features.Mediator.Events.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        { 
            //Entity den Results a eşleşme
            CreateMap<Event, GetByIdEventQueryResult>().ReverseMap();
            CreateMap<Event, GetEventQueryResult>().ReverseMap();
        }
    }
}
