using TicketBox.Application.Features.CQRS.Attendees.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class RemoveAttendeeCommandHandler
    {
        private readonly TicketContext _context;

        public RemoveAttendeeCommandHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task Handle(RemoveAttendeeCommand command)
        {
            var attendee = await _context.Attendees.FindAsync(command.AttendeeId);
            _context.Attendees.Remove(attendee);
            await _context.SaveChangesAsync();
        }
    }
}
