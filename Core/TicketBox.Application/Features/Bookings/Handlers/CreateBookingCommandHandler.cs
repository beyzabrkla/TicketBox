using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TicketBox.Application.Features.Bookings.Commands;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Bookings.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateBookingCommandHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<Unit> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;//httpContextAccessor.HttpContext: şu anda işlem yapan kişinin bilgilerini tutar, giriş yapılıp yapılmadığını kontrol etmek için kullanılır.
                                                                                                             //ClaimTypes.NameIdentifier: giriş yapan kişinin id'sini almak için kullanılır.

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("Kullanıcı girişi bulunamadı!");

            //Rezervasyonu oluştur
            var booking = new Booking
            {
                AppUserId = userId,
                BookingDate = request.BookingDate,
                TotalAmount = request.TotalAmount,
                EventId = request.EventId,
                Tickets = new List<Ticket>() // Listeyi başlatıyoruz
            };

            //Biletleri oluştur
            for (int i = 0; i < request.TicketCount; i++)
            {
                var ticket = new Ticket
                {
                    Booking = booking,
                    EventId = request.EventId,
                    AppUserId = userId,
                    Price = request.TotalAmount / request.TicketCount,
                    PurchaseDate = DateTime.UtcNow,
                    PNR = Guid.NewGuid().ToString()[..6].ToUpper(),
                    TicketCode = $"TCK-2026-{Guid.NewGuid().ToString()[..6].ToUpper()}",
                    IsActive = true,
                    IsUsed = false
                };
            }

            await _context.Bookings.AddAsync(booking, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
}
}
