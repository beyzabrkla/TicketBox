using FluentValidation;
using Microsoft.AspNetCore.Http;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Bookings.Commands.Validators
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        private readonly TicketContext _ticketContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateBookingCommandValidator(TicketContext ticketContext, IHttpContextAccessor httpContextAccessor)
        {
            _ticketContext = ticketContext;
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.EventId).NotEmpty().WithMessage("Etkinlik seçimi zorunludur.");
            RuleFor(x => x.TicketCount).GreaterThan(0).WithMessage("Bilet sayısı 0'dan büyük olmalıdır.");

            //Eğer Admin değilse kapasite kontrolüne takıl
            RuleFor(x => x)
                .MustAsync(async (cmd, ct) =>
                {
                    // Admin ise kuralı atla
                    if (IsAdmin()) return true;

                    return await BookingValidatorHelper.IsCapacityAvailable(_ticketContext, cmd.EventId, cmd.TicketCount, ct);
                })
                .WithMessage("Etkinlik kapasitesi yetersiz!");
        }

        private bool IsAdmin()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user != null && user.IsInRole("Admin");
        }
    }
}