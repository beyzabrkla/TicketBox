using MediatR;
using TicketBox.Application.Features.Categories.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Categories.Handlers
{
    public class RemoveCategoryCommandHandler :IRequestHandler<RemoveCategoryCommand>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public RemoveCategoryCommandHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category != null)
            {
                await _categoryRepository.RemoveAsync(category);
                await _categoryRepository.SaveChangesAsync();
            }
        }
    }
}
