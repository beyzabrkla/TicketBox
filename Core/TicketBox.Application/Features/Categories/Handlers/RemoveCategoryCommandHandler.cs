using MediatR;
using TicketBox.Application.Features.Categories.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Categories.Handlers
{
    public class RemoveCategoryCommandHandler :IRequestHandler<RemoveCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public RemoveCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(new object[] { request.CategoryId },cancellationToken);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
