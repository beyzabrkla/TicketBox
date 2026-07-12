using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketBox.Application.Features.Tickets.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Tickets.Handlers
{
    public class AdminAddTicketCommandHandler : IRequestHandler<AdminAddTicketCommand, Unit>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationDbContext _context;


        public AdminAddTicketCommandHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(AdminAddTicketCommand request, CancellationToken cancellationToken)
        {
            // Etkinliği bul
            var eventEntity = await _context.Events.FindAsync(new object[] { request.EventId }, cancellationToken);

            if (eventEntity == null)
                throw new Exception("Etkinlik bulunamadı.");

            // Sadece kapasiteyi artır
            eventEntity.Capacity += request.TicketCount;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
