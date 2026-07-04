using MediatR;
using TicketBox.Application.Features.Mediator.Events.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class RemoveEventCommandHandler : IRequestHandler<RemoveEventCommand>
    {
        private readonly TicketContext _ticketContext;
        public RemoveEventCommandHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task Handle(RemoveEventCommand request, CancellationToken cancellationToken)
        {
            var values = await _ticketContext.Events.FindAsync(request.EventId);
            _ticketContext.Events.Remove(values);
            await _ticketContext.SaveChangesAsync();
        }
    }
}
