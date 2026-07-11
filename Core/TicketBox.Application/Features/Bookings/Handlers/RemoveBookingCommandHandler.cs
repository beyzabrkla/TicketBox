using MediatR;
using TicketBox.Application.Features.Bookings.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Bookings.Handlers
{
    public class RemoveBookingCommandHandler : IRequestHandler<RemoveBookingCommand,Unit>
    {
        private readonly IApplicationDbContext _context;

        public RemoveBookingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(RemoveBookingCommand request, CancellationToken cancellationToken)
        {
            //nesneyi çekiyoruz
            var booking = await _context.Bookings.FindAsync(new object[] { request.BookingId },cancellationToken);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
