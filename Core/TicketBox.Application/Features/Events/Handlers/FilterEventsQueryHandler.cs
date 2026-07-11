using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Common.Specifications;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Application.Features.Events.Specifications;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class FilterEventsQueryHandler : IRequestHandler<FilterEventsQuery, List<GetEventQueryResult>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FilterEventsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GetEventQueryResult>> Handle(FilterEventsQuery request, CancellationToken cancellationToken)
        {
            //specification nesnesini oluştur
            var spec = new FilterEventsSpecification(
                    request.CategoryId,
                    request.IsActive,
                    request.MinPrice,
                    request.MaxPrice,
                    request.Upcoming,
                    request.SoldOut);

            //queryi başlat
            var query = _context.Events.AsQueryable();

            //spec ile sorguyu işle 
            var values = await SpecificationEvaluator<Event>.GetQuery(query, spec).ToListAsync(cancellationToken);

            return _mapper.Map<List<GetEventQueryResult>>(values);
        }
    }
}
