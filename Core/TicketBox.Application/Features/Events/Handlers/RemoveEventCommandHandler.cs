using MediatR;
using TicketBox.Application.Features.Events.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class RemoveEventCommandHandler : IRequestHandler<RemoveEventCommand>
    {
        private readonly IApplicationDbContext _context;

        public RemoveEventCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveEventCommand request, CancellationToken cancellationToken)
        {
            var values = await _context.Events.FindAsync(new object[] { request.EventId },cancellationToken);
            if (values != null)
            {
                _context.Events.Remove(values);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
