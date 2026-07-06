using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.CQRS.Categories.Queries;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class GetByIdCategoryQueryHandler
    {
        private readonly TicketContext _context;
        private readonly IMapper _mapper;

        public GetByIdCategoryQueryHandler(TicketContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetByIdCategoryQueryResult> Handle(GetCategoryByIdQuery query)
        {
            var category = await _context.Categories.Where(c => c.CategoryId == query.CategoryId).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCategoryQueryResult>(category);
        }
    }
}