using FluentValidation;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Bookings.Commands.Validators
{
    public class AdminCreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public AdminCreateBookingCommandValidator(TicketContext context)
        {
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.TicketCount).GreaterThan(0);

            RuleFor(x => x)
                .MustAsync(async (cmd, ct) => await BookingValidatorHelper.IsCapacityAvailable(context, cmd.EventId, cmd.TicketCount, ct))
                .WithMessage("DİKKAT: Etkinlik kapasitesi aşıldı, ancak yönetici yetkisiyle işlem yapılıyor.");
        }
    }
}
