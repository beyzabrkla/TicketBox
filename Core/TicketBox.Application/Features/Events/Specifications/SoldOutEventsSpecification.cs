using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Events.Specifications
{
    public class SoldOutEventsSpecification : BaseSpecification<Event>
    {
        public SoldOutEventsSpecification()
        {
            AddCriteria(x => x.Tickets.Count >= x.Capacity);

            AddInclude(x => x.Category);
            AddInclude(x => x.Tickets);
        }
    }
}
