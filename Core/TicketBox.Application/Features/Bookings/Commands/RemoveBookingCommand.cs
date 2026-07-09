using MediatR;

namespace TicketBox.Application.Features.Bookings.Commands
{
    public class RemoveBookingCommand :IRequest
    {
        public int BookingId { get; set; }
    }
}
