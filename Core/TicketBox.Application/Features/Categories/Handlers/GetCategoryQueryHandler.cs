using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Categories.Queries;
using TicketBox.Application.Features.Categories.Results;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Categories.Handlers
{
    public class GetCategoryQueryHandler :IRequestHandler<GetCategoryQuery, List<GetCategoryQueryResult>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<GetCategoryQueryResult>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                    .Include(x => x.Events)
                    .ProjectTo<GetCategoryQueryResult>(_mapper.ConfigurationProvider) //Veritabanından sadece ihtiyacın olan sütunları seçer
                    .ToListAsync(cancellationToken);
        }
    }
}
