using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Tickets.Specifications
{
    public class TicketByEventSpecification : BaseSpecification<Ticket>
    {
        public TicketByEventSpecification(int eventId)
        {
            AddInclude(t => t.AppUser);
            AddInclude(t => t.Event);
            AddInclude(t => t.Booking);
        }
    }
}
