namespace TicketBox.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendTicketEmailAsync(string email, string subject, string body);
    }
}
