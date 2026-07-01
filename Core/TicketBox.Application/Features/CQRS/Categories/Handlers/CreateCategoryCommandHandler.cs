using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class CreateCategoryCommandHandler
    {
        private readonly TicketContext _context;

        public CreateCategoryCommandHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateCategoryCommand command)
        {
            var category = new Category
            {
                CategoryName = command.CategoryName
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
    }
}
