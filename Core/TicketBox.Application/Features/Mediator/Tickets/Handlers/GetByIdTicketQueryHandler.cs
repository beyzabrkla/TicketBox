using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Mediator.Tickets.Queries;
using TicketBox.Application.Features.Mediator.Tickets.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Tickets.Handlers
{
    public class GetByIdTicketQueryHandler : IRequestHandler<GetByIdTicketQuery, GetByIdTicketQueryResult>
    {
        private readonly TicketContext _ticketContext;
        private readonly IMapper _mapper;

        public GetByIdTicketQueryHandler(TicketContext ticketContext, IMapper mapper)
        {
            _ticketContext = ticketContext;
            _mapper = mapper;
        }

        public async Task<GetByIdTicketQueryResult> Handle(GetByIdTicketQuery request, CancellationToken cancellationToken)
        {
            var value = await _ticketContext.Tickets.Where(x => x.TicketId == request.TicketId).FirstOrDefaultAsync(cancellationToken);
            return _mapper.Map<GetByIdTicketQueryResult>(value);
        }
    }
}
