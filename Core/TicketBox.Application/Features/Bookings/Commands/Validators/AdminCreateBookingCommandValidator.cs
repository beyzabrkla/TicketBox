using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Bookings.Commands.Validators
{
    public class AdminCreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        private readonly IApplicationDbContext _context;

        public AdminCreateBookingCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.TicketCount).GreaterThan(0);

            RuleFor(x => x)
                .MustAsync(async (cmd, ct) => await BookingValidatorHelper.IsCapacityAvailable(_context, cmd.EventId, cmd.TicketCount, ct))
                .WithMessage("DİKKAT: Etkinlik kapasitesi aşıldı, ancak yönetici yetkisiyle işlem yapılıyor.");
        }
    }
}
