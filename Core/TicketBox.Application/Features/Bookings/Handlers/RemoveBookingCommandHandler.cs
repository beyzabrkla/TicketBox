using MediatR;
using TicketBox.Application.Features.Bookings.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Bookings.Handlers
{
    public class RemoveBookingCommandHandler : IRequestHandler<RemoveBookingCommand>
    {
        private readonly IGenericRepository<Booking> _genericRepository;

        public RemoveBookingCommandHandler(IGenericRepository<Booking> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task Handle(RemoveBookingCommand request, CancellationToken cancellationToken)
        {
            var value = await _genericRepository.GetByIdAsync(request.BookingId);
            await _genericRepository.RemoveAsync(value);
            await _genericRepository.SaveChangesAsync();
        }
    }
}
