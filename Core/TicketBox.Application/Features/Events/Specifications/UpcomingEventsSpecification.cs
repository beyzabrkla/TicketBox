using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Events.Specifications
{
    public class UpcomingEventsSpecification : BaseSpecification<Event>
    {
        public UpcomingEventsSpecification()
        {
            AddCriteria(e=>e.EventDate > DateTime.UtcNow); //gelicek etkinlikler
        }
    }
}
