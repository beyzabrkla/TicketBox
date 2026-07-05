using MediatR;
using TicketBox.Application.Features.Mediator.Tickets.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Tickets.Handlers
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand>
    {
        private readonly TicketContext _ticketContext;

        public UpdateTicketCommandHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var values = await _ticketContext.Tickets.FindAsync(request.TicketId);
        
            if (values == null)
                return;
        
            _ticketContext.Tickets.Update(values);
            await _ticketContext.SaveChangesAsync();

        }
    }
}
