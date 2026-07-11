using MediatR;
using TicketBox.Application.Features.Categories.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Categories.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(new object[] { request.CategoryId }, cancellationToken);

            if (category == null)
            {
                throw new Exception("Kategori Bulunamadı!");
            }

            category.CategoryName = request.CategoryName;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}