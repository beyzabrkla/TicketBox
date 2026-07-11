using MediatR;
using TicketBox.Application.Features.Bookings.Commands;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Bookings.Handlers
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand,Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBookingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.FindAsync(new object[] { request.BookingId }, cancellationToken);

            if (booking == null)
                throw new Exception("Rezervasyon bulunamadı!");


            booking.BookingDate = request.BookingDate;
            booking.TotalAmount = request.TotalAmount;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
