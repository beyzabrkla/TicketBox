using FluentValidation;
using TicketBox.Application.Features.Tickets.Commands;

namespace TicketBox.Application.Features.Tickets.Commands.Validators
{
    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().WithMessage("Rezervasyon bilgisi zorunludur.");
            RuleFor(x => x.EventId).NotEmpty().WithMessage("Etkinlik bilgisi zorunludur.");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.");
        }
    }
}
