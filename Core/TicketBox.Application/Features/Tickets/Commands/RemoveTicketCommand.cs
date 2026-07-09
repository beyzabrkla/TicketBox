using MediatR;

namespace TicketBox.Application.Features.Tickets.Commands
{
    public class RemoveTicketCommand :IRequest
    {
        public int TicketId { get; set; }
    }
}
