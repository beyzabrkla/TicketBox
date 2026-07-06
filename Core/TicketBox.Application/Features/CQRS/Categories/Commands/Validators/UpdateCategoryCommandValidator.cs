using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Categories.Commands.Validators
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly TicketContext _ticketContext;
        public UpdateCategoryCommandValidator(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;

            RuleFor(x => x.CategoryName)
                    .NotEmpty().WithMessage("Kategori Adı Boş Olamaz!")
                    .MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.")
                    .MaximumLength(50).WithMessage("Kategori Adı 50 Karakterden Fazla Olamaz!")
                    .MustAsync(BeUniqueExceptCurrent).WithMessage("Bu Kategori Adı Zaten Kullanılmaktadır!");
        }

        private async Task<bool> BeUniqueExceptCurrent(UpdateCategoryCommand command, string categoryName, CancellationToken cancellationToken)
        {
            // Veritabanında ismi aynı olan ama ID'si bizimkinden farklı olan bir kayıt var mı?
            return !await _ticketContext.Categories
                .AnyAsync(c => c.CategoryName == command.CategoryName && c.CategoryId != command.CategoryId);
        }

    }
}
