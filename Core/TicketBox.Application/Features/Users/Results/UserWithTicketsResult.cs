namespace TicketBox.Application.Features.Users.Results
{
    public class UserWithTicketsResult
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int TicketCount { get; set; }
    }
}
