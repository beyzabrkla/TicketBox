using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Application.Features.Events.Specifications;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class GetPopularEventsQueryHandler : IRequestHandler<GetPopularEventsQuery, List<EventResult>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPopularEventsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EventResult>> Handle(GetPopularEventsQuery request, CancellationToken cancellationToken)
        {
            // Specification'ı çağır
            var spec = new PopularEventsSpecification(request.Count);

            return await _context.Events
                    .Where(spec.Criteria ?? (e => true)) // Criteria null olabilir, koruma ekledik
                    .OrderByDescending(x => x.Tickets.Count(t => t.IsActive))
                    .Take(request.Count)
                    .ProjectTo<EventResult>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
        }
    }
}