using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Tickets.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Tickets.Handlers
{
    public class RemoveTicketCommandHandler : IRequestHandler<RemoveTicketCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public RemoveTicketCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveTicketCommand request, CancellationToken cancellationToken)
        {
            // İşlemleri bir transaction ile başlatıyoruz ki veritabanı tutarsızlığı olmasın
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var ticket = await _context.Tickets
                    .Include(t => t.Booking)
                    .FirstOrDefaultAsync(t => t.TicketId == request.TicketId, cancellationToken);

                if (ticket == null) throw new Exception("Bilet bulunamadı.");
                if (!ticket.IsActive) throw new Exception("Bilet zaten iptal edilmiş.");

                ticket.IsActive = false;

                ticket.Booking.TicketCount -= 1;
                ticket.Booking.TotalAmount -= ticket.Price;

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
