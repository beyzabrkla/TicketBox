using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Events.Results;

namespace TicketBox.Application.Features.Mediator.Events.Queries
{
    public class GetByIdEventQuery : IRequest<GetByIdEventQueryResult>
    {
        public int Id { get; set; }
    }
}
