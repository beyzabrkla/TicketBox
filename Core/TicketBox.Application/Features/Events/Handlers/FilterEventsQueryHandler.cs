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
            var filteredQuery = SpecificationEvaluator<Event>.GetQuery(query, spec);

            //Toplam sayıyı al
            var totalCount = await filteredQuery.CountAsync(cancellationToken);

            //Filtrelenmiş etkinliklerin ID listesini alıp toplam satışı hesaplıyoruz
            var eventIds = await filteredQuery.Select(e => e.EventId).ToListAsync(cancellationToken);

            var totalSalesForTheseEvents = await _context.Bookings
                .Where(b => eventIds.Contains(b.EventId))
                .SumAsync(b => b.TotalAmount - b.ServiceFee, cancellationToken);

            //Sayfalama
            var values = await filteredQuery
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Include(e => e.Tickets) 
                    .Include(e => e.Category)
                    .Select(e => new GetEventQueryResult
                    {
                        EventId = e.EventId,
                        Title = e.Title,
                        Description = e.Description,
                        ImageUrl = e.ImageUrl,
                        Location = e.Location,
                        IsActive = e.IsActive,
                        Price = e.Price ?? 0,
                        EventDate = e.EventDate ?? DateTime.UtcNow,
                        CategoryName = e.Category.CategoryName,
                        CategoryId = e.CategoryId,
                        Capacity = e.Capacity ?? 0,
                        TicketCount = e.Tickets.Count(t => t.IsActive)
                    })
                    .ToListAsync(cancellationToken);

            return new PaginatedEventResult
            {
                Items = values, // Artık mapper'a gerek kalmadan doğrudan atayabilirsin
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalSalesAmount = totalSalesForTheseEvents
            };
        }
    }
}
