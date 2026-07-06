using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Common.Specifications;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Mediator.Tickets.Specifications
{
    public class UserTicketsSpecification : BaseSpecification<Ticket>
    {
        public UserTicketsSpecification(string userId)
        {
            AddCriteria(t => t.AppUserId == userId);
        }
    }
}
