using TicketBox.Application.Features.Bookings.Specifications;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Bookings.Commands.Validators
{
    public static class BookingValidatorHelper
    {
        public static async Task<bool> IsCapacityAvailable(IGenericRepository<Event> eventRepository, IGenericRepository<Booking> bookingRepository, int eventId, int requestedCount, CancellationToken ct)
        {
            var eventEntity = await eventRepository.GetByIdAsync(eventId); //burada eventId ile eşleşen etkinliği buluyoruz
            if (eventEntity == null) return false;

            //O etkinliğe ait rezervasyonları çeken bir specification
            var bookings = await bookingRepository.ListAsync(new BookingsByEventSpecification(eventId), ct);

            var currentBookings = bookings.Sum(x => x.Tickets.Count);

            return (currentBookings + requestedCount) <= eventEntity.Capacity; //toplam rezervasyon sayısı + istenen bilet sayısı etkinlik kapasitesini aşmıyorsa true döndür
        }
    }
}
