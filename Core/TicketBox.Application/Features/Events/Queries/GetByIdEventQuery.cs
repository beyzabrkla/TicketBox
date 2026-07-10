using MediatR;
using TicketBox.Application.Features.Events.Results;

namespace TicketBox.Application.Features.Events.Queries
{
    public class GetByIdEventQuery : IRequest<GetByIdEventQueryResult>
    {
        public int Id { get; set; }
    }
}
