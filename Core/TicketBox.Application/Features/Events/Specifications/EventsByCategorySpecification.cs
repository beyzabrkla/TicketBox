using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Events.Specifications
{
    public class EventsByCategorySpecification : BaseSpecification<Event>
    {
        public EventsByCategorySpecification(int categoryId)
        {
            AddCriteria(x => x.CategoryId == categoryId);

            AddInclude(x => x.Category);
            AddInclude(x => x.Tickets);
        }
    }
}
