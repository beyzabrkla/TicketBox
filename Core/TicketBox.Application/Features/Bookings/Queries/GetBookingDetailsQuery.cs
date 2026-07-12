using MediatR;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Bookings.Queries
{
    public class GetBookingDetailsQuery : IRequest<Booking>
    {
        public int Id { get; set; }
    }
}
