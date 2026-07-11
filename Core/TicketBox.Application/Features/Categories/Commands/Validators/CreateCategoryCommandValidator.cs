using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;
namespace TicketBox.Application.Features.Categories.Commands.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.CategoryName)
                            .NotEmpty().WithMessage("Kategori adı boş olamaz!")
                            .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
                            .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olabilir.")
                            // Veritabanı kontrolü
                            .MustAsync(BeUniqueCategoryName).WithMessage("Bu kategori adı zaten mevcut!");

            RuleFor(x => x.IconUrl)
                .NotEmpty().WithMessage("Kategori Fotoğrafı boş olamaz");
        }

        // Asenkron veritabanı kontrol metodu
        private async Task<bool> BeUniqueCategoryName(string categoryName, CancellationToken cancellationToken)
        {
            // bu isimde bir kategori var mı?
            //      YOKSA           //t tipindeki değişken Category sınıfına çevriliyor ve categoryName özelliğine erişiyoruz
            return !await _context.Categories.AnyAsync(c => ((Category)(object)c).CategoryName == categoryName, cancellationToken);
        }
    }
}

