using AutoMapper;
using MediatR;
using TicketBox.Application.Features.Bookings.Queries;
using TicketBox.Application.Features.Bookings.Results;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Bookings.Handlers
{
    public class GetByIdBookingQueryHandler : IRequestHandler<GetByIdBookingQuery, GetByIdBookingQueryResult>
    {
        private readonly IApplicationDbContext _context; 
        private readonly IMapper _mapper;

        public GetByIdBookingQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetByIdBookingQueryResult> Handle(GetByIdBookingQuery request, CancellationToken cancellationToken)
        {
          var value = await _context.Bookings.FindAsync(new object[] { request.BookingId }, cancellationToken);
            return _mapper.Map<GetByIdBookingQueryResult>(value);
        }
    }
}
