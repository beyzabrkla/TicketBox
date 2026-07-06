using AutoMapper;
using TicketBox.Application.Features.Mediator.Tickets.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, GetByIdTicketQueryResult>().ReverseMap();
            CreateMap<Ticket, GetTicketQueryResult>().ReverseMap();
        }
    }
}
