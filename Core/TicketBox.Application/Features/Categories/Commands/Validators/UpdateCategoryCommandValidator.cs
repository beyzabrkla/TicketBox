using FluentValidation;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Categories.Commands.Validators
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public UpdateCategoryCommandValidator(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(x => x.CategoryName)
                    .NotEmpty().WithMessage("Kategori Adı Boş Olamaz!")
                    .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
                    .MaximumLength(50).WithMessage("Kategori Adı 50 Karakterden Fazla Olamaz!")
                    .MustAsync(BeUniqueExceptCurrent).WithMessage("Bu Kategori Adı Zaten Kullanılmaktadır!");
            _categoryRepository = categoryRepository;
        }

        private async Task<bool> BeUniqueExceptCurrent(UpdateCategoryCommand command, string categoryName, CancellationToken cancellationToken)
        {
            // Veritabanında ismi aynı olan ama ID'si bizimkinden farklı olan bir kayıt var mı?
            return !await _categoryRepository.AnyAsync(
                            c => c.CategoryName == command.CategoryName && c.CategoryId != command.CategoryId,
                            cancellationToken);
        }

    }
}
