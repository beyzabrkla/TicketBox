using AutoMapper;
using TicketBox.Application.Features.Tickets.Commands;
using TicketBox.Application.Features.Tickets.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    // Mapping/TicketProfile.cs
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, GetByIdTicketQueryResult>();
            CreateMap<Ticket, GetTicketQueryResult>();

            CreateMap<CreateTicketCommand, Ticket>();

            CreateMap<UpdateTicketCommand, Ticket>()
                .ForMember(dest => dest.TicketId, opt => opt.Ignore())
                .ForMember(dest => dest.PNR, opt => opt.Ignore());
        }
    }
}
