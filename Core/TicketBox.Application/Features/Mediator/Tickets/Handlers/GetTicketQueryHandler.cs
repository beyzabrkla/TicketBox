using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Mediator.Tickets.Queries;
using TicketBox.Application.Features.Mediator.Tickets.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Tickets.Handlers
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, List<GetTicketQueryResult>>
    {
        private readonly TicketContext _ticketContext;

        public GetTicketQueryHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var values = await _ticketContext.Tickets
                .Select(t => new GetTicketQueryResult
                {
                    TicketId = t.TicketId,
                    EventId = t.EventId,
                    AttendeeId = t.AttendeeId,
                    PurchaseDate = t.PurchaseDate,
                    Price = t.Price
                })
                .ToListAsync(cancellationToken);

            return values;
        }
    }
}
