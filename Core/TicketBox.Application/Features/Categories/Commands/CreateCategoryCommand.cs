using MediatR;

namespace TicketBox.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand :IRequest<Unit>
    {
        public string CategoryName { get; set; }
    }
}
