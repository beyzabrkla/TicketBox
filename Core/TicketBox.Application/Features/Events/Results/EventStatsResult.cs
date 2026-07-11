namespace TicketBox.Application.Features.Events.Results
{
    public class EventStatsResult
    {
        public int ActiveEventCount { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public int SoldOutEventCount { get; set; }
        public int DraftEventCount { get; set; }
    }
}
