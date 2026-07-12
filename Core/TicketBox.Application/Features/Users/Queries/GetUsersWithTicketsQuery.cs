using MediatR;
using TicketBox.Application.Features.Users.Results;

namespace TicketBox.Application.Features.Users.Queries
{
    public class GetUsersWithTicketsQuery : IRequest<List<UserWithTicketsResult>>
    {
    }
}
