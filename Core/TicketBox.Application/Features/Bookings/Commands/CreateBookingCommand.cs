using MediatR;

namespace TicketBox.Application.Features.Bookings.Commands
{
    public class CreateBookingCommand :IRequest<int> //int dönücek
    {   
        public DateTime BookingDate { get; set; }
        public int EventId { get; set; }
        public int TicketCount { get; set; }
        public string AppUserId { get; set; }
    }
}
