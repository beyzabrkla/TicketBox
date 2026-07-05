using MediatR;

namespace TicketBox.Application.Features.Mediator.Bookings.Commands
{
    public class RemoveBookingCommand :IRequest
    {
        public int BookingId { get; set; }
    }
}
