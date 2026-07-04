using TicketBox.Application.Features.CQRS.Attendees.Commands;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.CQRS.Attendees.Handlers
{
    public class UpdateAttendeeCommandHandler
    {
        private readonly TicketContext _context;

        public UpdateAttendeeCommandHandler(TicketContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateAttendeeCommand command)
        {
            var attendee = await _context.Attendees.FindAsync(command.AttendeeId);
            attendee.Name = command.Name;
            attendee.Surname = command.Surname;
            attendee.Email = command.Email;
            await _context.SaveChangesAsync();
        }
    }
}
