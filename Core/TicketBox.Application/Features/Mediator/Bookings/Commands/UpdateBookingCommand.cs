using MediatR;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Bookings.Commands
{
    public class UpdateBookingCommand :IRequest
    {
        public int BookingId { get; set; }
        public string AppUserId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
