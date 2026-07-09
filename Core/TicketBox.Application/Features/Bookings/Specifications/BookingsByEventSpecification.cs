using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Bookings.Specifications
{
    public class BookingsByEventSpecification :BaseSpecification<Booking>
    {
        public BookingsByEventSpecification(int eventId)
        {
            // Veritabanına bu filtreyi uygular
            AddCriteria(x => x.EventId == eventId);

            // Ticket listesini çekmek için
            AddInclude(x => x.Tickets);
        }
    }
}
