using MediatR;
using TicketBox.Application.Features.Bookings.Results;

namespace TicketBox.Application.Features.Bookings.Queries
{
    public class GetByIdBookingQuery :IRequest<GetByIdBookingQueryResult>
    {
        public int BookingId { get; set; }
    }
}
