using AutoMapper;
using TicketBox.Application.Features.Mediator.Bookings.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, GetBookingQueryResult>().ReverseMap();
            CreateMap<Booking, GetByIdBookingQueryResult>().ReverseMap();
        }
    }
}
