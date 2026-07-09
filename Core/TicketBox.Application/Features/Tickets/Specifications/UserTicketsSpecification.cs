using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Tickets.Specifications
{
    public class UserTicketsSpecification : BaseSpecification<Ticket>
    {
        public UserTicketsSpecification(string userId)
        {
            AddCriteria(t => t.AppUserId == userId);
        }
    }
}
