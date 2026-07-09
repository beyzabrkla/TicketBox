using FluentValidation;
using Microsoft.AspNetCore.Http;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Bookings.Commands.Validators
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IGenericRepository<Booking> _bookingRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateBookingCommandValidator(IHttpContextAccessor httpContextAccessor, IGenericRepository<Booking> bookingRepository, IGenericRepository<Event> eventRepository)
        {
            _bookingRepository = bookingRepository;
            _eventRepository = eventRepository;
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.EventId).NotEmpty().WithMessage("Etkinlik seçimi zorunludur.");
            RuleFor(x => x.TicketCount).GreaterThan(0).WithMessage("Bilet sayısı 0'dan büyük olmalıdır.");

            //Eğer Admin değilse kapasite kontrolüne takıl
            RuleFor(x => x)
                        .MustAsync(async (cmd, ct) =>
                        {
                            if (IsAdmin()) return true;

                            return await BookingValidatorHelper.IsCapacityAvailable(
                                _eventRepository,
                                _bookingRepository,
                                cmd.EventId,
                                cmd.TicketCount,
                                ct);
                        })
                        .WithMessage("Etkinlik kapasitesi yetersiz!");

            _bookingRepository = bookingRepository;
            _eventRepository = eventRepository;
        }

        private bool IsAdmin()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user != null && user.IsInRole("Admin");
        }
    }
}