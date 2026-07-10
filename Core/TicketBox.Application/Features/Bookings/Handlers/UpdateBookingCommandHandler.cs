using MediatR;
using TicketBox.Application.Features.Bookings.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Bookings.Handlers
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand,Unit>
    {
        private readonly IGenericRepository<Booking> _bookingRepository;

        public UpdateBookingCommandHandler(IGenericRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Unit> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null)
                throw new Exception("Bilet bulunamadı!"); // Pipeline burada da Exception fırlatıp kullanıcıya hata döner

            booking.BookingDate = request.BookingDate;
            booking.TotalAmount = request.TotalAmount;

            await _bookingRepository.UpdateAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
