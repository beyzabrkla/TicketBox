using MediatR;

namespace TicketBox.Application.Features.Categories.Commands
{
    public class RemoveCategoryCommand :IRequest
    {
        public int CategoryId { get; set; }
    }
}
