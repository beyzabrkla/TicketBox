using Microsoft.EntityFrameworkCore;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Bookings.Commands.Validators
{
    public static class BookingValidatorHelper
    {
        public static async Task<bool> IsCapacityAvailable(TicketContext context, int eventId, int requestedCount, CancellationToken ct)
        {
            var eventEntity = await context.Events.FindAsync(eventId); //burada eventId ile eşleşen etkinliği buluyoruz
            if (eventEntity == null) return false;

            var currentBookings = await context.Bookings // burada eventId ile eşleşen tüm rezervasyonları alıyoruz ve toplam bilet sayısını hesaplıyoruz
                .Where(x => x.EventId == eventId)
                .SumAsync(x => x.Tickets.Count, ct);

            return (currentBookings + requestedCount) <= eventEntity.Capacity; //   toplam rezervasyon sayısı + istenen bilet sayısı etkinlik kapasitesini aşmıyorsa true döndür
        }
    }
}
