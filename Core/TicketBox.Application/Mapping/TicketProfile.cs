using AutoMapper;
using TicketBox.Application.Features.Tickets.Commands;
using TicketBox.Application.Features.Tickets.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, GetByIdTicketQueryResult>();
            CreateMap<Ticket, GetTicketQueryResult>();
           
            CreateMap<Ticket, GetTicketQueryResult>()
                        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.Name + " " + src.AppUser.Surname))
                        .ForMember(dest => dest.Booking, opt => opt.MapFrom(src => src.Booking))
                        .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event));
        }
    }
}
