using MediatR;
using TicketBox.Application.Features.Events.Results;

namespace TicketBox.Application.Features.Events.Queries
{
    public class GetEventQuery : IRequest<List<GetEventQueryResult>>
    {
        public string? SearchTerm { get; set; }
        public List<int>? CategoryIds { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? Date { get; set; }
        public int Page { get; set; } = 1;     
        public int PageSize { get; set; } = 6; 
        public string? SortBy { get; set; }
    }
}
