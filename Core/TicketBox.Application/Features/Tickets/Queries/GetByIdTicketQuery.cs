using MediatR;
using TicketBox.Application.Features.Tickets.Results;

namespace TicketBox.Application.Features.Tickets.Queries
{
    public class GetByIdTicketQuery : IRequest<GetByIdTicketQueryResult>
    {
        public int TicketId { get; set; }
    }
}
