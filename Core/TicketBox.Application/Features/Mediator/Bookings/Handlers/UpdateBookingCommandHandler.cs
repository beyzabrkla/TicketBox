using MediatR;
using TicketBox.Application.Features.Mediator.Bookings.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Bookings.Handlers
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand>
    {
        private readonly TicketContext _ticketContext;

        public UpdateBookingCommandHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var values = await _ticketContext.Bookings.FindAsync(request.BookingId);
            if (values == null) 
                return;

            _ticketContext.Bookings.Remove(values);
            await _ticketContext.SaveChangesAsync();
        }
    }
}
