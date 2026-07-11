using MediatR;

namespace TicketBox.Application.Features.Bookings.Commands
{
    public class RemoveBookingCommand :IRequest<Unit>
    {
        public int BookingId { get; set; }
    }
}
