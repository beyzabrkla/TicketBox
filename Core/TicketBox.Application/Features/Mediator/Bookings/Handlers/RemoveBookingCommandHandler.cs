using MediatR;
using TicketBox.Application.Features.Mediator.Bookings.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Bookings.Handlers
{
    public class RemoveBookingCommandHandler : IRequestHandler<RemoveBookingCommand>
    {
        private readonly TicketContext _ticketContext;

        public RemoveBookingCommandHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }
        public async Task Handle(RemoveBookingCommand request, CancellationToken cancellationToken)
        {
            var value = await _ticketContext.Bookings.FindAsync(request.BookingId);
            _ticketContext.Bookings.Remove(value);
            await _ticketContext.SaveChangesAsync(cancellationToken);
        }
    }
}
