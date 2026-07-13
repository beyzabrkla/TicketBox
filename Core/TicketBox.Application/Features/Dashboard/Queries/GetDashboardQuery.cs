using MediatR;
using TicketBox.Application.Features.Dashboard.Results;

namespace TicketBox.Application.Features.Dashboard.Queries
{
    public record GetDashboardQuery : IRequest<DashboardQueryResult>;

}
