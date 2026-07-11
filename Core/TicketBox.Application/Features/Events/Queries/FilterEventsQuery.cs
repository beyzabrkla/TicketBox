using MediatR;
using TicketBox.Application.Features.Events.Results;

namespace TicketBox.Application.Features.Events.Queries
{
    public class FilterEventsQuery : IRequest<PaginatedEventResult>
    {
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool Upcoming { get; set; }
        public bool SoldOut { get; set; }
        public string? SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
