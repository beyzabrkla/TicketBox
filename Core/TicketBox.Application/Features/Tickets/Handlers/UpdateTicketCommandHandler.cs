using MediatR;
using TicketBox.Application.Features.Tickets.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Tickets.Handlers
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand>
    {
        private readonly IGenericRepository<Ticket> _ticketRepository;

        public UpdateTicketCommandHandler(IGenericRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var values = await _ticketRepository.GetByIdAsync(request.TicketId);
        
            if (values == null)
            {
                throw new Exception("Bilet Bulunamadı!");
            }        
            await _ticketRepository.UpdateAsync(values);
            await _ticketRepository.SaveChangesAsync();

        }
    }
}
