using MediatR;
using TicketBox.Application.Features.Events.Results;

namespace TicketBox.Application.Features.Events.Queries
{
    public class FilterEventsQuery : IRequest<List<GetEventQueryResult>>
    {
        public int? CategoryId { get; set; }

        public bool? IsActive { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public bool Upcoming { get; set; }

        public bool SoldOut { get; set; }
    }
}
