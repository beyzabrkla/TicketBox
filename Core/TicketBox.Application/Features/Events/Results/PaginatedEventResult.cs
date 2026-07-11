namespace TicketBox.Application.Features.Events.Results
{
    public class PaginatedEventResult
    {
        public List<GetEventQueryResult> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
