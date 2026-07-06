using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Categories.Commands.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly TicketContext _ticketContext;
        public CreateCategoryCommandValidator(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;

            RuleFor(x => x.CategoryName)
                            .NotEmpty().WithMessage("Kategori adı boş olamaz!")
                            .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
                            .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olabilir.")
                            // Veritabanı kontrolü:
                            .MustAsync(BeUniqueCategoryName).WithMessage("Bu kategori adı zaten mevcut!");
        }

        // Asenkron veritabanı kontrol metodu
        private async Task<bool> BeUniqueCategoryName(string categoryName, CancellationToken cancellationToken)
        {
            return !await _ticketContext.Categories.AnyAsync(c => c.CategoryName == categoryName, cancellationToken);
        }
    }
}

