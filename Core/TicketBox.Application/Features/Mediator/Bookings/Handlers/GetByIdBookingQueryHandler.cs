using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Mediator.Bookings.Queries;
using TicketBox.Application.Features.Mediator.Bookings.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Bookings.Handlers
{
    public class GetByIdBookingQueryHandler : IRequestHandler<GetByIdBookingQuery, GetByIdBookingQueryResult>
    {
        private readonly TicketContext _ticketContext;

        public GetByIdBookingQueryHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task<GetByIdBookingQueryResult> Handle(GetByIdBookingQuery request, CancellationToken cancellationToken)
        {
          var value = await _ticketContext.Bookings
                .Where(b => b.BookingId == request.BookingId)
                .Select(b => new GetByIdBookingQueryResult
                {
                    BookingId = b.BookingId,
                    AppUserId = b.AppUserId,
                    BookingDate = b.BookingDate,
                    TotalAmount = b.TotalAmount,
                    EventId = b.EventId,
                    Event = b.Event,
                    Tickets = b.Tickets.ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
            return value;
        }
    }
}
