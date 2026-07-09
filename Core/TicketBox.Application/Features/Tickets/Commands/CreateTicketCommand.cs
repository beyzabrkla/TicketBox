using MediatR;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Tickets.Commands
{
    public class CreateTicketCommand :IRequest
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public string AppUserId { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
    }
}
