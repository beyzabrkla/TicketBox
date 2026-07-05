using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                AttendeeId = request.AttendeeId,
                PurchaseDate = request.PurchaseDate,
                Price = request.Price
            };
            await _ticketContext.Tickets.AddAsync(tickets);
            await _ticketContext.SaveChangesAsync();

        }
    }
}
