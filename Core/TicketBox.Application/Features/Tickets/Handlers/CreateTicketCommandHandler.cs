using MediatR;
using TicketBox.Application.Features.Tickets.Commands;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Tickets.Handlers
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand,Unit>
    {
        private readonly IApplicationDbContext _context;

        public CreateTicketCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var tickets = new Ticket
            {
                EventId = request.EventId,
                BookingId = request.BookingId,
                AppUserId = request.AppUserId,
                PurchaseDate = request.PurchaseDate,
                Price = request.Price,

                PNR = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(), // Sistem üretiyor
                TicketCode = $"TCK-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}" // Sistem üretiyor
            };
            await _context.Tickets.AddAsync(tickets);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
