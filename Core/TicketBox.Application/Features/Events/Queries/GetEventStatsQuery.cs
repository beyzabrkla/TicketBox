using MediatR;
using TicketBox.Application.Features.Events.Results;

namespace TicketBox.Application.Features.Events.Queries
{
    public class GetEventStatsQuery : IRequest<EventStatsResult>
    {
    }
}
