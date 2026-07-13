using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Tickets.Queries;
using TicketBox.Application.Features.Tickets.Results;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Tickets.Handlers
{
    public class GetTicketsByEventIdQueryHandler : IRequestHandler<GetTicketsByEventIdQuery, List<GetTicketQueryResult>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTicketsByEventIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetTicketsByEventIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tickets
                .Include(t => t.AppUser)
                .Include(t => t.Event)
                .Include(t => t.Booking)
                .Where(t => t.EventId == request.EventId)
                .Select(t => new GetTicketQueryResult
                {
                    TicketId = t.TicketId,
                    BookingId = t.BookingId,
                    Booking = t.Booking,
                    AppUserId = t.AppUserId,
                    UserName = t.AppUser.Name + " " + t.AppUser.Surname,
                    EventId = t.EventId,
                    Event = t.Event, 
                    IsActive = t.IsActive,
                    PurchaseDate = t.PurchaseDate,
                    PNR = t.PNR,
                    TicketCode = t.TicketCode,
                    Price = t.Price
                })
                .ToListAsync(cancellationToken);
        }
    }
}
