namespace TicketBox.Application.Features.Events.Results
{
    public class GetEventQueryResult
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TicketCount { get; set; }
        public double OccupancyRate => Capacity > 0 ? (double)TicketCount / Capacity * 100 : 0;
    }
}
