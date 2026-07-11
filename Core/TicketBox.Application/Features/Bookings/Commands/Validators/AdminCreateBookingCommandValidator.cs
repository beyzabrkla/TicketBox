using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Bookings.Commands.Validators
{
    public class AdminCreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        private readonly DbContext _context;
        public AdminCreateBookingCommandValidator(DbContext context)
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
