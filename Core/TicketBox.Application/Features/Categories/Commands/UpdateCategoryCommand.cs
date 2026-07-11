using MediatR;

namespace TicketBox.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand :IRequest<Unit>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string IconName { get; set; }
        public string IconUrl { get; set; }
    }
}
