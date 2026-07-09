using FluentValidation;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;
namespace TicketBox.Application.Features.Categories.Commands.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        public CreateCategoryCommandValidator(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(x => x.CategoryName)
                            .NotEmpty().WithMessage("Kategori adı boş olamaz!")
                            .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
                            .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olabilir.")
                            // Veritabanı kontrolü
                            .MustAsync(BeUniqueCategoryName).WithMessage("Bu kategori adı zaten mevcut!");
        }

        // Asenkron veritabanı kontrol metodu
        private async Task<bool> BeUniqueCategoryName(string categoryName, CancellationToken cancellationToken)
        {
            // bu isimde bir kategori var mı?
            //      YOKSA           //t tipindeki değişken Category sınıfına çevriliyor ve categoryName özelliğine erişiyoruz
            return !await _categoryRepository.AnyAsync(c => ((Category)(object)c).CategoryName == categoryName, cancellationToken);
        }
    }
}

