using TicketBox.Application.Features.CQRS.Categories.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class UpdateCategoryCommandHandler
    {
        private readonly TicketContext _context;

        public UpdateCategoryCommandHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCategoryCommand command)
        {
            var category = await _context.Categories.FindAsync(command.CategoryId);
            category.CategoryName = command.CategoryName;
            await _context.SaveChangesAsync();
        }
    }
}
