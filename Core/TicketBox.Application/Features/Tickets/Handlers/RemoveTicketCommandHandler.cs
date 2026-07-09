using MediatR;
using TicketBox.Application.Features.Tickets.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Tickets.Handlers
{
    public class RemoveTicketCommandHandler : IRequestHandler<RemoveTicketCommand>
    {
        private readonly IGenericRepository<Ticket> _ticketRepository;

        public RemoveTicketCommandHandler(IGenericRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Handle(RemoveTicketCommand request, CancellationToken cancellationToken)
        {
            var value = await _ticketRepository.GetByIdAsync(request.TicketId);

            if (value != null)
            {
                await _ticketRepository.RemoveAsync(value);
                await _ticketRepository.SaveChangesAsync();
            }
        }
    }
}
