using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.CQRS.Attendees.Queries;
using TicketBox.Application.Features.CQRS.Attendees.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class GetAttendeeByIdQueryHandler
    {
        private readonly TicketContext _context;

        public GetAttendeeByIdQueryHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task<GetAttendeeByIdQueryResult> Handle(GetAttendeeByIdQuery query)
        {
            var attendee = await _context.Attendees.Where(a => a.AttendeeId == query.Id)
                                                    .Select(x => new GetAttendeeByIdQueryResult
                                                    {
                                                        AttendeeId = x.AttendeeId,
                                                        Name = x.Name,
                                                        Surname = x.Surname,
                                                        Email = x.Email
                                                    }).FirstOrDefaultAsync();
            return attendee;
        }
    }
}
