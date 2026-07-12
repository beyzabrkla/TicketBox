using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<Ticket> Tickets { get; set; }
        DbSet<ApplicationUser> Users { get; set; }
        DatabaseFacade Database { get; } // Transaction için 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }

}
