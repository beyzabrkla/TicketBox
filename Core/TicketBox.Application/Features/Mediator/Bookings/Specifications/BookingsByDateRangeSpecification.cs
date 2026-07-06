using TicketBox.Application.Features.Common.Specifications;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Bookings.Specifications
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
