using FluentValidation;

namespace TicketBox.Application.Features.Events.Commands.Validators
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200).WithMessage("Başlık boş geçilemez.");
            RuleFor(x => x.EventDate).GreaterThan(DateTime.Now).WithMessage("Etkinlik tarihi geçmiş olamaz.");
            RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("Kapasite en az 1 olmalıdır.");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori seçimi zorunludur.");
        }
    }
}
