using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Users.Specifications
{
    public class GetUsersWithTicketsSpecification :BaseSpecification<ApplicationUser>
    {
        public GetUsersWithTicketsSpecification()
        {
            AddInclude(u => u.Tickets.Where(t => t.IsActive == true));
        }
    }
}
