using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Categories.Handlers
{
    public class GetCategoryQueryHandler
    {
        private readonly TicketContext _context;

        public GetCategoryQueryHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task<List<GetCategoryQueryResult>> Handle()
        {
            var values = await _context.Categories
                                .Select(x => new GetCategoryQueryResult
                                {
                                    CategoryId = x.CategoryId,
                                    CategoryName = x.CategoryName
                                }).ToListAsync();
            return values;
        }
    }
}
