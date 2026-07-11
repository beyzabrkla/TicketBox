using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Bookings.Commands.Validators
{
    public static class BookingValidatorHelper
    {
        public static async Task<bool> IsCapacityAvailable(IApplicationDbContext _context, int eventId, int requestedCount, CancellationToken ct)
        {
            // Event'i çekerken Tickets koleksiyonunu da dahil ediyoruz
            var eventEntity = await _context.Events
                .Include(e => e.Tickets)
                .FirstOrDefaultAsync(e => e.EventId == eventId, ct);

            if (eventEntity == null) return false;

            // Etkinliğin mevcut bilet sayısı Tickets koleksiyonunun sayısı
            var currentSoldTickets = eventEntity.Tickets?.Count ?? 0;

            // Kapasite kontrolü
            return (currentSoldTickets + requestedCount) <= eventEntity.Capacity;
        }
    }
}
