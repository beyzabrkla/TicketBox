namespace TicketBox.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? IconName { get; set; } 
        public string? IconUrl { get; set; }
        public List<Event> Events { get; set; }
    }
}
