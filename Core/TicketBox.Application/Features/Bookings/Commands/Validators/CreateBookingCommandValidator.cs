using FluentValidation;
using Microsoft.AspNetCore.Http;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Bookings.Commands.Validators
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateBookingCommandValidator(IHttpContextAccessor httpContextAccessor, IApplicationDbContext context)
        { 
            _httpContextAccessor = httpContextAccessor;
            _context = context;

            RuleFor(x => x.EventId).NotEmpty().WithMessage("Etkinlik seçimi zorunludur.");
            RuleFor(x => x.TicketCount).GreaterThan(0).WithMessage("Bilet sayısı 0'dan büyük olmalıdır.");

            //Eğer Admin değilse kapasite kontrolüne takıl
            RuleFor(x => x).MustAsync(async (cmd, ct) =>
                            {
                                // Admin ise kontrolü geç
                                if (IsAdmin()) return true;

                                return await BookingValidatorHelper.IsCapacityAvailable(_context, cmd.EventId, cmd.TicketCount, ct);
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