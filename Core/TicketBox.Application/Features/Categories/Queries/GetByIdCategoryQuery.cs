using MediatR;
using TicketBox.Application.Features.Categories.Results;

namespace TicketBox.Application.Features.Categories.Queries
{
    public class GetByIdCategoryQuery :IRequest<GetByIdCategoryQueryResult>
    {
        public int CategoryId { get; set; }
    }
}
 