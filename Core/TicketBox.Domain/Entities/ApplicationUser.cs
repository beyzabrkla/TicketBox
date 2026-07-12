using Microsoft.AspNetCore.Identity;

namespace TicketBox.Domain.Entities
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
