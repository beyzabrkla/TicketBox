using AutoMapper;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        { 
            //Entity den Results a eşleşme
            CreateMap<Event, GetByIdEventQueryResult>().ReverseMap();
            CreateMap<Event, GetEventQueryResult>()
                .ForMember(x => x.CategoryName,
                    opt => opt.MapFrom(x => x.Category.CategoryName))
                .ForMember(x => x.TicketCount,
                    opt => opt.MapFrom(x => x.Tickets.Count)).ReverseMap();
        }
    }
}
