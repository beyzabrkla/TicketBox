using MediatR;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Bookings.Commands
{
    public class UpdateBookingCommand :IRequest
    {
        public int BookingId { get; set; }
        public int NewTicketCount { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
