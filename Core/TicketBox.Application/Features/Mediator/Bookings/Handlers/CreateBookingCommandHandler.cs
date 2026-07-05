using MediatR;
using TicketBox.Application.Features.Mediator.Bookings.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Bookings.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand>
    {
        private readonly TicketContext _ticketContext;

        public CreateBookingCommandHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = new Booking
            {
                AppUserId = request.AppUserId,
                BookingDate = request.BookingDate,
                TotalAmount = request.TotalAmount,
                EventId = request.EventId,
                Tickets = request.Tickets
            };
            await _ticketContext.Bookings.AddAsync(booking);
            await _ticketContext.SaveChangesAsync();
        }
    }
}
