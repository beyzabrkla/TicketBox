using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Mediator.Bookings.Queries;
using TicketBox.Application.Features.Mediator.Bookings.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Bookings.Handlers
{
    public class GetByIdBookingQueryHandler : IRequestHandler<GetByIdBookingQuery, GetByIdBookingQueryResult>
    {
        private readonly TicketContext _ticketContext;
        private readonly IMapper _mapper;

        public GetByIdBookingQueryHandler(TicketContext ticketContext, IMapper mapper)
        {
            _ticketContext = ticketContext;
            _mapper = mapper;
        }

        public async Task<GetByIdBookingQueryResult> Handle(GetByIdBookingQuery request, CancellationToken cancellationToken)
        {
          var value = await _ticketContext.Bookings.Where(b => b.BookingId == request.BookingId).FirstOrDefaultAsync(cancellationToken);
            return _mapper.Map<GetByIdBookingQueryResult>(value);
        }
    }
}
