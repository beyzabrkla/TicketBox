using MediatR;

namespace TicketBox.Application.Features.Mediator.Tickets.Commands
{
    public class RemoveTicketCommand :IRequest
    {
        public int TicketId { get; set; }
    }
}
