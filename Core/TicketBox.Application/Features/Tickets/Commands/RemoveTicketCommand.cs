using MediatR;

namespace TicketBox.Application.Features.Tickets.Commands
{
    public class RemoveTicketCommand :IRequest<Unit>
    {
        public int TicketId { get; set; }
        public string UserId { get; set; }
    }
}
