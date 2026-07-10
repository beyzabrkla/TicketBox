using MediatR;

namespace TicketBox.Application.Features.Bookings.Commands
{
    public class CreateBookingCommand :IRequest<Unit>
    {   
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int EventId { get; set; }
        public int TicketCount { get; set; } 
    }
}
