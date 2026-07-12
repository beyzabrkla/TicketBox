using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Persistance.Context
{
    public class TicketContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public override DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticket>()
                .HasOne(t => t.AppUser)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}