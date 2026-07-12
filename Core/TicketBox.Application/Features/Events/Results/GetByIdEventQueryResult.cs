namespace TicketBox.Application.Features.Events.Results
{
    public class GetByIdEventQueryResult
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public decimal ServiceFee { get; set; } 
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int SoldTicketCount { get; set; }
        public int RemainingCapacity => Capacity - SoldTicketCount;
    }
}
