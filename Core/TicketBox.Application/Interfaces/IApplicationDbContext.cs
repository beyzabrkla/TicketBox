using Microsoft.EntityFrameworkCore;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<Ticket> Tickets { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
