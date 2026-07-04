using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.CQRS.Attendees.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class GetAttendeeQueryHandler
    {
        private readonly TicketContext _context;

        public GetAttendeeQueryHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task<List<GetAttendeeQueryResult>> Handle()
        {
            var values = await _context.Attendees
                                .Select(x => new GetAttendeeQueryResult
                                {
                                  AttendeeId = x.AttendeeId,
                                  Name = x.Name,
                                  Surname= x.Surname,
                                  Email = x.Email
                                }).ToListAsync();
            return values;
        }
    }
}
