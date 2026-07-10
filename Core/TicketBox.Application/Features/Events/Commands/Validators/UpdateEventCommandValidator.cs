using FluentValidation;

namespace TicketBox.Application.Features.Events.Commands.Validators
{
    public class UpdateEventCommandValidator :AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200).WithMessage("Başlık boş geçilemez.");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500).WithMessage("Açıklama gereklidir ve en fazla 500 karakter olabilir.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Bir resim URL'i zorunludur.");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Konum bilgisi boş geçilemez.");
            RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("Kapasite en az 1 olmalıdır.");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori seçimi zorunludur.");

            // Etkinlik tarihi geçmişe çekilemez
            RuleFor(x => x.EventDate).GreaterThan(DateTime.Now).WithMessage("Geçmiş bir tarihe etkinlik güncellenemez.");
        }
    }
}
