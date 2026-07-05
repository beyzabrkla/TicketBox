using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Mediator.Bookings.Queries;
using TicketBox.Application.Features.Mediator.Bookings.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Bookings.Handlers
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, List<GetBookingQueryResult>>
    {
        private readonly TicketContext _ticketContext;

        public GetBookingQueryHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task<List<GetBookingQueryResult>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var values = await _ticketContext.Bookings
                .Select(b => new GetBookingQueryResult
                {
                    BookingId = b.BookingId,
                    AppUserId = b.AppUserId,
                    BookingDate = b.BookingDate,
                    TotalAmount = b.TotalAmount,
                    EventId = b.EventId,
                    Event = b.Event,
                    Tickets = b.Tickets.ToList()
                })
                .ToListAsync(cancellationToken);
            
            return values;
        }
    }
}
