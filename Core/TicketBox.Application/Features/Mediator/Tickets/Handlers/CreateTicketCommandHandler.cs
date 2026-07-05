using MediatR;
using TicketBox.Application.Features.Mediator.Tickets.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Tickets.Handlers
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand>
    {
        private readonly TicketContext _ticketContext;

        public CreateTicketCommandHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
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
                PNR = request.PNR,
                TicketCode = request.TicketCode
            };
            await _ticketContext.Tickets.AddAsync(tickets);
            await _ticketContext.SaveChangesAsync();

        }
    }
}
