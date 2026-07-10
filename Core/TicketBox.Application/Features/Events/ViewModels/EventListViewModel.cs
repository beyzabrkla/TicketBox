using TicketBox.Application.Features.Events.Commands;
using TicketBox.Application.Features.Events.Results;

namespace TicketBox.Application.Features.Events.ViewModels
{
    public class EventListViewModel
    {
        public List<GetEventQueryResult> Events { get; set; }
        public CreateEventCommand CreateEventCommand { get; set; }
    }
}
