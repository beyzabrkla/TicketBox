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
            var value = await _context.Tickets.FindAsync(new object[] { request.TicketId },cancellationToken);

            if (value != null)
            {
                _context.Tickets.Remove(value);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
