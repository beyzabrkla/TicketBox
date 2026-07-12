using MediatR;

namespace TicketBox.Application.Features.Tickets.Commands
{
    public class AdminAddTicketCommand : IRequest<Unit>
    {
        public int EventId { get; set; }
        public int TicketCount { get; set; }
    }
}

