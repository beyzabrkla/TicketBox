using MediatR;
using TicketBox.Application.Features.Tickets.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Tickets.Handlers
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand>
    {
        private readonly IGenericRepository<Ticket> _ticketRepository;

        public CreateTicketCommandHandler(IGenericRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var tickets = new Ticket
            {
                EventId = request.EventId,
                BookingId = request.BookingId,
                AppUserId = request.AppUserId,
                PurchaseDate = request.PurchaseDate,
                Price = request.Price,

                PNR = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(), // Sistem üretiyor
                TicketCode = $"TCK-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}" // Sistem üretiyor
            };
            await _ticketRepository.AddAsync(tickets);
            await _ticketRepository.SaveChangesAsync();

        }
    }
}
