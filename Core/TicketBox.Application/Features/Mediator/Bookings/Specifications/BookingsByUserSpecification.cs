using TicketBox.Application.Features.Common.Specifications;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Bookings.Specifications
{
    public class BookingsByUserSpecification : BaseSpecification<Booking>
    {
        public BookingsByUserSpecification(string userId) 
        {
            // Belirli kullanıcının rezervasyonlarını filtrele
            AddCriteria(b => b.AppUserId == userId);

            // Rezervasyonla ilgili Etkinlik bilgilerini de beraberinde getir
            AddInclude(b => b.Event);
        }
    }
}
