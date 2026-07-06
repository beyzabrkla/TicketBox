using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class GetCategoryQueryHandler
    {
        private readonly TicketContext _context;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(TicketContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetCategoryQueryResult>> Handle()
        {
            var values = await _context.Categories.ToListAsync();
            return _mapper.Map<List<GetCategoryQueryResult>>(values);
        }
    }
}
