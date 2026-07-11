using MediatR;
using TicketBox.Application.Features.Events.Results;

namespace TicketBox.Application.Features.Events.Queries
{
    public class GetPopularEventsQuery : IRequest<List<EventResult>>
    {
        public int Count { get; set; }

        // Constructor ekleyerek hatayı gideriyoruz
        public GetPopularEventsQuery(int count)
        {
            Count = count;
        }
    }
}
