using MediatR;
using TicketBox.Application.Features.Mediator.Events.Results;

namespace TicketBox.Application.Features.Mediator.Events.Queries
{
    public class GetEventQuery :IRequest<List<GetEventQueryResult>> 
    {
    }
}
