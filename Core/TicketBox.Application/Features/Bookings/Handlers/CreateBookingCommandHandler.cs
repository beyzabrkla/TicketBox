using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("Kullanıcı girişi bulunamadı!");

            //Etkinliği ve fiyatını çek
            var eventEntity = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

            if (eventEntity == null || !eventEntity.IsActive)
                throw new Exception("Etkinlik bulunamadı veya pasif durumda!");

            //Kapasite kontrolü
            var soldTicketsCount = await _context.Tickets
                .Where(t => t.EventId == request.EventId)
                .SumAsync(t => t.Quantity, cancellationToken);

            if (soldTicketsCount + request.TicketCount > eventEntity.Capacity)
                throw new Exception("Yeterli kontenjan bulunmamaktadır!");

            // Fiyatı heesapla
            decimal unitPrice = eventEntity.Price ?? 0;

            decimal serviceFee = 150; // Bu değeri bir konfigürasyondan da çekebilirsin
            decimal totalAmount = (unitPrice * request.TicketCount) + serviceFee;
            
            // Rezervasyonu oluştur (Tarihi sistemden al)
            var booking = new Booking
            {
                AppUserId = userId,
                BookingDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                ServiceFee = serviceFee,
                EventId = request.EventId,
                Tickets = new List<Ticket>()
            };

            // Biletleri oluştur
            for (int i = 0; i < request.TicketCount; i++)
            {
                var ticket = new Ticket
                {
                    Booking = booking,
                    EventId = request.EventId,
                    AppUserId = userId,
                    Price = unitPrice, // Bilet başına gerçek fiyat
                    PurchaseDate = DateTime.UtcNow,
                    PNR = Guid.NewGuid().ToString()[..6].ToUpper(),
                    TicketCode = $"TCK-2026-{Guid.NewGuid().ToString()[..6].ToUpper()}",
                    IsActive = true,
                    IsUsed = false,
                    Quantity = 1
                };

                booking.Tickets.Add(ticket);
            }

            await _context.Bookings.AddAsync(booking, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}