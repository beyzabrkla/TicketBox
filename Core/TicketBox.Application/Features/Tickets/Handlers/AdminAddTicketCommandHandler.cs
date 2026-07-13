using MediatR;
using Microsoft.AspNetCore.Http;
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
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.IsInRole("Admin"))
                throw new UnauthorizedAccessException("Yetkisiz işlem!");

            var eventEntity = await _context.Events.FindAsync(new object[] { request.EventId }, cancellationToken);

            if (eventEntity == null)
                throw new Exception("Etkinlik bulunamadı.");

            eventEntity.Capacity += request.TicketCount; //kapasite arttırma
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
