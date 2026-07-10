using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Events.Specifications
{
    public class ActiveEventsSpecification : BaseSpecification<Event>
    {
        public ActiveEventsSpecification()
        {
            AddCriteria(x => x.IsActive);

            AddInclude(x => x.Category);
            AddInclude(x => x.Tickets);
        }
    }
}
