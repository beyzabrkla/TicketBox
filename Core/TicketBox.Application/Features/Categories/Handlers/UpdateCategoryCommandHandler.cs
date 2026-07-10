using MediatR;
using TicketBox.Application.Features.Categories.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Categories.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand,Unit>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public UpdateCategoryCommandHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (category == null)
            {
                throw new Exception("Kategori Bulunamadı!"); // Pipeline burada hatayı yakalar
            }

            category.CategoryName = request.CategoryName;
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return Unit.Value; //başarılı sonucu dön
        }
    }
}
