using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Application.Features.CQRS.Attendees.Commands
{
    public class RemoveAttendeeCommand
    {
        public int AttendeeId { get; set; }
    }
}
