using MediatR;

namespace TicketBox.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand :IRequest
    {
        public string CategoryName { get; set; }
    }
}
