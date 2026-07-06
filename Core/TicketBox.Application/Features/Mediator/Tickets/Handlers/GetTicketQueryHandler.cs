using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Mediator.Tickets.Queries;
using TicketBox.Application.Features.Mediator.Tickets.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Tickets.Handlers
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, List<GetTicketQueryResult>>
    {
        private readonly TicketContext _ticketContext;
        private readonly IMapper _mapper;

        public GetTicketQueryHandler(TicketContext ticketContext, IMapper mapper)
        {
            _ticketContext = ticketContext;
            _mapper = mapper;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var values = await _ticketContext.Tickets.ToListAsync(cancellationToken);

            return _mapper.Map<List<GetTicketQueryResult>>(values);
        }
    }
}
