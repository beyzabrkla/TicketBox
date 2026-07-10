using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Events.Specifications
{
    public class EventListSpecification : BaseSpecification<Event>
    {
        public EventListSpecification()
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Tickets);
        }
    }
}
