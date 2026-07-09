using MediatR;

namespace TicketBox.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand :IRequest
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
