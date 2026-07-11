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
    public class FilterEventsQueryHandler : IRequestHandler<FilterEventsQuery, PaginatedEventResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FilterEventsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginatedEventResult> Handle(FilterEventsQuery request, CancellationToken cancellationToken)
        {
            // Eğer request.CategoryId null değilse listeye al, null ise boş liste gönder
            var categoryList = request.CategoryId.HasValue
                                                        ? new List<int> { request.CategoryId.Value }
                                                        : new List<int>();
            var spec = new FilterEventsSpecification(
                    categoryList,
                    request.IsActive,
                    request.MinPrice,
                    request.MaxPrice,
                    request.Upcoming,
                    request.SoldOut,
                    request.SearchTerm);

            var query = _context.Events.AsQueryable();

            // Specification üzerinden sorguyu filtrele
            var filteredQuery = SpecificationEvaluator<Event>.GetQuery(query, spec);

            //Toplam sayıyı al (Sayfalama öncesi)
            var totalCount = await filteredQuery.CountAsync(cancellationToken);

            //Sayfalama
            var values = await filteredQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            //Sonucu eşleyip PaginatedEventResult dön
            return new PaginatedEventResult
            {
                Items = _mapper.Map<List<GetEventQueryResult>>(values),
                TotalCount = totalCount
            };
        }
    }
}
