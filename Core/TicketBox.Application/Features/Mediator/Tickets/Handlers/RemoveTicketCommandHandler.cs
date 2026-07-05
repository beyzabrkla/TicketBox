using MediatR;
using TicketBox.Application.Features.Mediator.Tickets.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Tickets.Handlers
{
    public class RemoveTicketCommandHandler : IRequestHandler<RemoveTicketCommand>
    {
        private readonly TicketContext _ticketContext;

        public RemoveTicketCommandHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task Handle(RemoveTicketCommand request, CancellationToken cancellationToken)
        {
            var value = await _ticketContext.Tickets.FindAsync(request.TicketId);
            _ticketContext.Tickets.Remove(value);
            await _ticketContext.SaveChangesAsync(cancellationToken);
        }
    }
}
