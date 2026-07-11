using MediatR;
using TicketBox.Application.Features.Tickets.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Tickets.Handlers
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand,Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTicketCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var values = await _context.Tickets.FindAsync(new object[] { request.TicketId },cancellationToken);
        
            if (values == null)
            {
                throw new Exception("Bilet Bulunamadı!");
            }        
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
