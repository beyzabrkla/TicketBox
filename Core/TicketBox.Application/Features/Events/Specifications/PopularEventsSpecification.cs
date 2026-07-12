using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Events.Specifications
{
    public class PopularEventsSpecification : BaseSpecification<Event>
    {
        public PopularEventsSpecification(int count)
        {
            ApplyOrderByDescending(x => x.Tickets.Count(t => t.IsActive));

            ApplyPaging(0, count); // Burada Take mantığını kur
        }
    }
}
