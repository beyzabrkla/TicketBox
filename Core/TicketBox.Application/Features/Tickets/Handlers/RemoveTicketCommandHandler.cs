using MediatR;
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
            var ticket = await _context.Tickets.FindAsync(new object[] { request.TicketId }, cancellationToken);

            if (ticket == null)
                throw new Exception("Bilet bulunamadı.");

            // İptal edilmiş mi zaten diye kontrol
            if (!ticket.IsActive)
                throw new Exception("Bilet zaten iptal edilmiş.");

            var eventItem = await _context.Events.FindAsync(new object[] { ticket.EventId }, cancellationToken);

            if (eventItem != null)
            {
                // Kapasiteyi artır
                eventItem.Capacity += 1;
            }

            //Silmek yerine pasife al
            ticket.IsActive = false;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
