using AutoMapper;
using TicketBox.Application.Features.Bookings.Commands;
using TicketBox.Application.Features.Bookings.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, GetBookingQueryResult>().ReverseMap();
            CreateMap<Booking, GetByIdBookingQueryResult>().ReverseMap();

            CreateMap<CreateBookingCommand, Booking>()
                .ForMember(dest => dest.Tickets, opt => opt.Ignore()) // Tickets elle eklenecek
                .ForMember(dest => dest.Event, opt => opt.Ignore());

            CreateMap<UpdateBookingCommand, Booking>()
                .ForMember(dest => dest.Tickets, opt => opt.Ignore());
        }
    }
}
