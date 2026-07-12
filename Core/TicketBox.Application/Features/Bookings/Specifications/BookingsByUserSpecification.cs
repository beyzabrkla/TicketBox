using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Bookings.Specifications
{
    public class BookingsByUserSpecification : BaseSpecification<Booking>
    {
        public BookingsByUserSpecification(string userId) 
        {
            // Belirli kullanıcının rezervasyonlarını filtrele
            AddCriteria(b => b.AppUserId == userId);

            // Rezervasyonla ilgili Etkinlik bilgilerini de beraberinde getir
            AddInclude(b => b.Event);

            //son yapılan rezervasyon en başta
            ApplyOrderByDescending(b => b.BookingDate);
        }
    }
}
