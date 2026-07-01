using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.CQRS.Categories.Queries;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class GetByIdCategoryQueryHandler
    {
        private readonly TicketContext _context;

        public GetByIdCategoryQueryHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task<GetByIdCategoryQueryResult> Handle(GetCategoryByIdQuery query)
        {
            var category = await _context.Categories.Where(c => c.CategoryId == query.CategoryId)
                                                     .Select(x=> new GetByIdCategoryQueryResult{
                                                         CategoryId = x.CategoryId,
                                                         CategoryName = x.CategoryName
                                                     }).FirstOrDefaultAsync();
            return category;
        }
    }
}