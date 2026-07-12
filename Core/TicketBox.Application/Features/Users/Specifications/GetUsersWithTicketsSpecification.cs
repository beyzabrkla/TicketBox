using TicketBox.Domain.Entities;
using TicketBox.Domain.Specifications;

namespace TicketBox.Application.Features.Users.Specifications
{
    public class GetUsersWithTicketsSpecification :BaseSpecification<ApplicationUser>
    {
        public GetUsersWithTicketsSpecification()
        {
            //hem biletleri olanları filtrele 
            AddCriteria(u => u.Tickets.Any());
 
            //hem de bilet verilerini yükle
            AddInclude(u => u.Tickets);
        }
    }
}
