using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Events.Specifications
{
    public class EventsByPriceSpecification : BaseSpecification<Event>
    {
        public EventsByPriceSpecification(decimal minPrice, decimal maxPrice)
        {
            AddCriteria(e => e.Price >= minPrice && e.Price <= maxPrice); //ücret filtrelemesi
        }
    }
}
