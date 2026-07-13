using AutoMapper;
using TicketBox.Application.Features.Events.Commands;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, GetByIdEventQueryResult>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<Event, GetEventQueryResult>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.CategoryName))
                .ForMember(x => x.TicketCount, opt => opt.MapFrom(x => x.Tickets != null ? x.Tickets.Count : 0));

            CreateMap<Event, EventResult>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.IsFastSelling, opt => opt.MapFrom(src => src.Tickets.Count(t => t.IsActive) >= (src.Capacity * 0.8)));
            CreateMap<CreateEventCommand, Event>();

            CreateMap<UpdateEventCommand, Event>()
                .ForMember(dest => dest.EventId, opt => opt.Ignore());
        }
    }
}