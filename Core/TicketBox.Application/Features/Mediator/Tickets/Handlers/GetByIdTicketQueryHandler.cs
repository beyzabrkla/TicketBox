using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Mediator.Tickets.Queries;
using TicketBox.Application.Features.Mediator.Tickets.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Tickets.Handlers
{
    public class GetByIdTicketQueryHandler : IRequestHandler<GetByIdTicketQuery, GetByIdTicketQueryResult>
    {
        private readonly TicketContext _ticketContext;

        public GetByIdTicketQueryHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task<GetByIdTicketQueryResult> Handle(GetByIdTicketQuery request, CancellationToken cancellationToken)
        {
            var value = await _ticketContext.Tickets.Where(x => x.TicketId == request.TicketId)
                .Select(x => new GetByIdTicketQueryResult
                {
                    TicketId = x.TicketId,
                    EventId = x.EventId,
                    Booking = x.Booking,
                    AppUserId = x.AppUserId,
                    PNR = x.PNR,
                    TicketCode = x.TicketCode,
                    PurchaseDate = x.PurchaseDate,
                    Price = x.Price
                }).FirstOrDefaultAsync(cancellationToken);
            return value;
        }
    }
}
