using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Events.Specifications
{
    public class FilterEventsSpecification : BaseSpecification<Event>
    {
        public FilterEventsSpecification(
         int? categoryId,
         bool? isActive,
         decimal? minPrice,
         decimal? maxPrice,
         bool upcoming,
         bool soldOut)
        {
            AddCriteria(x =>

                (!categoryId.HasValue || x.CategoryId == categoryId.Value)

                &&

                (!isActive.HasValue || x.IsActive == isActive.Value)

                &&

                (!minPrice.HasValue || x.Price >= minPrice.Value)

                &&

                (!maxPrice.HasValue || x.Price <= maxPrice.Value)

                &&

                (!upcoming || x.EventDate > DateTime.Now)

                &&

                (!soldOut || x.Tickets.Count >= x.Capacity)

            );

            AddInclude(x => x.Category);
            AddInclude(x => x.Tickets);
        }
    }
}
