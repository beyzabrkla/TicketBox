using MediatR;
using TicketBox.Application.Features.Categories.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Categories.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CreateCategoryCommandHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                CategoryName = request.CategoryName
            };
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
