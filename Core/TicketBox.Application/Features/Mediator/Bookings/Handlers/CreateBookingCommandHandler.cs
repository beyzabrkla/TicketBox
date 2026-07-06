using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TicketBox.Application.Features.Mediator.Bookings.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Bookings.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand>
    {
        private readonly TicketContext _ticketContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateBookingCommandHandler(TicketContext ticketContext, IHttpContextAccessor httpContextAccessor)
        {
            _ticketContext = ticketContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //httpContextAccessor.HttpContext: şu anda işlem yapan kişinin bilgilerini tutar, giriş yapılıp yapılmadığını kontrol etmek için kullanılır.
                                                                                                            //ClaimTypes.NameIdentifier: giriş yapan kişinin id'sini almak için kullanılır.

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("Kullanıcı girişi bulunamadı!");
            }

            var booking = new Booking
            {
                AppUserId = userId,
                BookingDate = request.BookingDate,
                TotalAmount = request.TotalAmount,
                EventId = request.EventId,
            };

            await _ticketContext.Bookings.AddAsync(booking, cancellationToken);
            await _ticketContext.SaveChangesAsync(cancellationToken); // BookingId oluştu

            //Şimdi oluşan id ile biletleri oluştur
            for (int i = 0; i < request.TicketCount; i++)
            {
                var ticket = new Ticket
                {
                    BookingId = booking.BookingId,
                    EventId = request.EventId,
                    AppUserId = userId,
                    Price = request.TotalAmount / request.TicketCount,
                    PurchaseDate = DateTime.UtcNow,
                    PNR = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(),
                    TicketCode = $"TCK-2026-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}",
                    IsActive = true,
                    IsUsed = false
                };

                _ticketContext.Tickets.Add(ticket);
            }
            await _ticketContext.SaveChangesAsync(cancellationToken);
        }
    }
}
