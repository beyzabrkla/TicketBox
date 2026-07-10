using FluentValidation;

namespace TicketBox.Application.Features.Events.Commands.Validators
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.Title)
                        .NotEmpty().WithMessage("Başlık boş geçilemez.")
                        .MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalı.");

            RuleFor(x => x.EventDate)
                .NotEmpty().WithMessage("Etkinlik tarihi zorunludur.")
                .GreaterThan(DateTime.Now).WithMessage("Etkinlik tarihi geçmiş olamaz.");

            RuleFor(x => x.Capacity)
                .NotEmpty().WithMessage("Kapasite boş olamaz.")
                .GreaterThan(0).WithMessage("Kapasite en az 1 olmalıdır.");

            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.");
            
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori seçimi zorunludur.");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop) // İlk hata çıkarsa durur
                .NotEmpty().WithMessage("Açıklama alanı boş bırakılamaz.")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Bir resim URL'i zorunludur.");
            
            RuleFor(x => x.Location).NotEmpty().WithMessage("Konum bilgisi boş geçilemez.");
        }
    }
}
