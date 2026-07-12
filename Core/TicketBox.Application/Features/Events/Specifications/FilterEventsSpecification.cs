using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Events.Specifications
{
    public class FilterEventsSpecification : BaseSpecification<Event>
    {
        public FilterEventsSpecification(List<int>? categoryIds, bool? isActive, decimal? minPrice, decimal? maxPrice, bool upcoming, bool soldOut, string? searchTerm, int page = 1, int pageSize = 6)
        {
            AddCriteria(x =>
                    // Burası artık liste kontrolü yapıyor:
                    (categoryIds == null || !categoryIds.Any() || categoryIds.Contains(x.CategoryId)) &&
                    (!isActive.HasValue || x.IsActive == isActive.Value) &&
                    (!minPrice.HasValue || x.Price >= minPrice.Value) &&
                    (!maxPrice.HasValue || x.Price <= maxPrice.Value) &&
                    (!upcoming || x.EventDate > DateTime.Now) &&
                    (!soldOut || x.Tickets.Count >= x.Capacity) &&
                    (string.IsNullOrEmpty(searchTerm) || x.Title.Contains(searchTerm) || x.Location.Contains(searchTerm) || x.Description.Contains(searchTerm))
                );

            AddInclude(x => x.Category);
            AddInclude(x => x.Tickets);
        }
    }
}
