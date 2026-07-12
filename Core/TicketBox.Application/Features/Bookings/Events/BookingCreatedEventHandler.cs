using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using TicketBox.Application.Features.Bookings.Events;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

public class BookingCreatedEventHandler : INotificationHandler<BookingCreatedEvent>
{
    private readonly IEmailService _emailService;
    private readonly IRazorViewToStringRenderer _viewRenderer;
    private readonly IServiceScopeFactory _scopeFactory;

    public BookingCreatedEventHandler(IEmailService emailService, IRazorViewToStringRenderer viewRenderer, IServiceScopeFactory scopeFactory)
    {
        _emailService = emailService;
        _viewRenderer = viewRenderer;
        _scopeFactory = scopeFactory;
    }


    public async Task Handle(BookingCreatedEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByIdAsync(notification.AppUserId);

            if (user == null) return;

            // Biletleri birleştirip tek bir HTML oluşturmak için StringBuilder kullanıyoruz
            var emailBodyBuilder = new StringBuilder();

            // Tüm biletleri dön ve her birini render et
            foreach (var ticket in notification.Booking.Tickets)
            {
                var ticketHtml = await _viewRenderer.RenderViewToStringAsync("_TicketCard", ticket);
                emailBodyBuilder.Append(ticketHtml);
            }

            // Tüm biletlerin HTML'i artık emailBody değişkeninde
            string emailBody = emailBodyBuilder.ToString();

            // Maili gönder
            await _emailService.SendTicketEmailAsync(user.Email, "Biletleriniz Hazır!", emailBody);
        }
    }
}