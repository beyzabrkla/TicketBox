using FluentValidation;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Bookings.Commands.Validators
{
    public class AdminCreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IGenericRepository<Booking> _bookingRepository;
        public AdminCreateBookingCommandValidator(IGenericRepository<Event> eventRepository, IGenericRepository<Booking> bookingRepository)
        {
            _eventRepository = eventRepository;
            _bookingRepository = bookingRepository;

            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.TicketCount).GreaterThan(0);

            RuleFor(x => x)
                .MustAsync(async (cmd, ct) => await BookingValidatorHelper.IsCapacityAvailable(_eventRepository, _bookingRepository, cmd.EventId, cmd.TicketCount, ct))
                .WithMessage("DİKKAT: Etkinlik kapasitesi aşıldı, ancak yönetici yetkisiyle işlem yapılıyor.");
        }
    }
}
