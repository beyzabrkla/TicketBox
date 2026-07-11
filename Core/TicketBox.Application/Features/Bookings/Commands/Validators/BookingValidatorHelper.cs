using Microsoft.EntityFrameworkCore;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Bookings.Commands.Validators
{
    public static class BookingValidatorHelper
    {
        public static async Task<bool> IsCapacityAvailable(DbContext _context, int eventId, int requestedCount, CancellationToken ct)
        {
            // Event'i çekerken Tickets koleksiyonunu da dahil ediyoruz
            var eventEntity = await _context.Set<Event>()
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
