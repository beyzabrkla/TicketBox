using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Events.Specifications
{
    public class EventsByPriceSpecification : BaseSpecification<Event>
    {
        public EventsByPriceSpecification(decimal minPrice, decimal maxPrice)
        {
            AddCriteria(x => x.Price >= minPrice && x.Price <= maxPrice);

            AddInclude(x => x.Category);
            AddInclude(x => x.Tickets);
        }
    }
}
