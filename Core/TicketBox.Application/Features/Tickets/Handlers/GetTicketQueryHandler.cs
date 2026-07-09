using AutoMapper;
using MediatR;
using TicketBox.Application.Features.Tickets.Queries;
using TicketBox.Application.Features.Tickets.Results;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Tickets.Handlers
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, List<GetTicketQueryResult>>
    {
        private readonly IGenericRepository<Ticket> _ticketRepository;
        private readonly IMapper _mapper;

        public GetTicketQueryHandler(IGenericRepository<Ticket> ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            var values = await _ticketRepository.GetAllAsync();

            return _mapper.Map<List<GetTicketQueryResult>>(values);
        }
    }
}
