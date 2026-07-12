namespace TicketBox.Application.Features.Categories.Results
{
    public class GetByIdCategoryQueryResult
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int EventCount { get; set; }
        public string IconName { get; set; }
        public string IconUrl { get; set; }
    }
}
