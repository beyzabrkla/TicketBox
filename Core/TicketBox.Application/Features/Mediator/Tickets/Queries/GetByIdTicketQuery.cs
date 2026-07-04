using MediatR;
using TicketBox.Application.Features.Mediator.Tickets.Results;

namespace TicketBox.Application.Features.Mediator.Tickets.Queries
{
    public class GetByIdTicketQuery : IRequest<GetByIdTicketQueryResult>
    {
        public int TicketId { get; set; }
    }
}
