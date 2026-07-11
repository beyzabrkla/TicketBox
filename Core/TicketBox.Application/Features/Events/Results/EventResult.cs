namespace TicketBox.Application.Features.Events.Results
{
    public class EventResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public bool IsFastSelling { get; set; }
    }
}
