using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Bookings.Specifications
{
    public class BookingsByDateRangeSpecification : BaseSpecification<Booking>
    {
        public BookingsByDateRangeSpecification(DateTime startDate, DateTime endDate)
        {
            // Başlangıç ve bitiş tarihleri arasındaki rezervasyonları filtrele
            AddCriteria(b => b.BookingDate >= startDate && b.BookingDate <= endDate);
        }
    }
}
