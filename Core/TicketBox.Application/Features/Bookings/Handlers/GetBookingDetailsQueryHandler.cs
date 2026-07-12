using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Bookings.Queries;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Bookings.Handlers
{
    public class GetBookingDetailsQueryHandler : IRequestHandler<GetBookingDetailsQuery, Booking>
    {
        private readonly IApplicationDbContext _context;

        public GetBookingDetailsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> Handle(GetBookingDetailsQuery request, CancellationToken cancellationToken)
        {
            // Booking'i ve içindeki tüm biletleri/etkinlikleri getir
            var booking = await _context.Bookings
                .Include(b => b.Tickets)
                    .ThenInclude(t => t.Event) // Biletin içindeki Event verisi gelmesi için şart
                .FirstOrDefaultAsync(b => b.BookingId == request.Id, cancellationToken);

            if (booking == null)
                throw new Exception("Rezervasyon bulunamadı.");

            return booking;
        }
    }
}