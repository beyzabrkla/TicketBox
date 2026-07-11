using AutoMapper;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, GetByIdEventQueryResult>()
                        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                        .ReverseMap();

            CreateMap<Event, GetEventQueryResult>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.CategoryName))
                .ForMember(x => x.TicketCount, opt => opt.MapFrom(x => x.Tickets.Count)).ReverseMap();

            CreateMap<Event, EventResult>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.IsFastSelling, opt => opt.MapFrom(src => src.Tickets.Count(t => t.IsActive) >= (src.Capacity * 0.8)));
        }
    }
}
