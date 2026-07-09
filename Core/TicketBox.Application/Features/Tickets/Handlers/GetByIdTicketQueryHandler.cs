using AutoMapper;
using MediatR;
using TicketBox.Application.Features.Tickets.Queries;
using TicketBox.Application.Features.Tickets.Results;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Tickets.Handlers
{
    public class GetByIdTicketQueryHandler : IRequestHandler<GetByIdTicketQuery, GetByIdTicketQueryResult>
    {
        private readonly IGenericRepository<Ticket> _ticketRepository;
        private readonly IMapper _mapper;

        public GetByIdTicketQueryHandler(IGenericRepository<Ticket> ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdTicketQueryResult> Handle(GetByIdTicketQuery request, CancellationToken cancellationToken)
        {
            var value = await _ticketRepository.GetByIdAsync(request.TicketId);
            return _mapper.Map<GetByIdTicketQueryResult>(value);
        }
    }
}
