using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TicketBox.Application.Features.Bookings.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Bookings.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Unit>
    {
        private readonly IGenericRepository<Booking> _bookingRepository;
        private readonly IGenericRepository<Ticket> _ticketRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateBookingCommandHandler(IGenericRepository<Booking> bookingRepository, IGenericRepository<Ticket> ticketRepository, IHttpContextAccessor httpContextAccessor)
        {
            _bookingRepository = bookingRepository;
            _ticketRepository = ticketRepository;
            _httpContextAccessor = httpContextAccessor;
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
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();

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
            await _ticketRepository.AddAsync(ticket);
        }

        //Her şey aynı anda commit edilir
        await _bookingRepository.SaveChangesAsync();
            return Unit.Value;
        }
}
}
