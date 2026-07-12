using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Tickets.Specifications
{
    public class UserTicketsSpecification : BaseSpecification<Ticket>
    {
        public UserTicketsSpecification(string userId, bool? isActive = null)
        {
            AddCriteria(t => t.AppUserId == userId);

            // Eğer bir değer gönderilirse filtrele gönderilmezse hepsini getir
            if (isActive.HasValue)
            {
                AddCriteria(t => t.IsActive == isActive.Value);
            }

            AddInclude(t => t.Booking);
            AddInclude(t => t.Event);
            AddInclude(t => t.Event.Category); 

            ApplyOrderByDescending(t => t.PurchaseDate);
        }
    }
}
