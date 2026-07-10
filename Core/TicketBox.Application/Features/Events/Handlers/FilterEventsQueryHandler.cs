using AutoMapper;
using MediatR;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Application.Features.Events.Specifications;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class FilterEventsQueryHandler : IRequestHandler<FilterEventsQuery, List<GetEventQueryResult>>
    {
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public FilterEventsQueryHandler(IGenericRepository<Event> eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<List<GetEventQueryResult>> Handle(FilterEventsQuery request, CancellationToken cancellationToken)
        {
            var values = await _eventRepository.ListAsync(
                new FilterEventsSpecification(
                    request.CategoryId,
                    request.IsActive,
                    request.MinPrice,
                    request.MaxPrice,
                    request.Upcoming,
                    request.SoldOut),
                cancellationToken);

            return _mapper.Map<List<GetEventQueryResult>>(values);
        }
    }
}
