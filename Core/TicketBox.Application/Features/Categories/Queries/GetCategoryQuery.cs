using MediatR;
using TicketBox.Application.Features.Categories.Results;

namespace TicketBox.Application.Features.Categories.Queries
{
    public class GetCategoryQuery :IRequest<List<GetCategoryQueryResult>>
    {
    }
}
