using AutoMapper;
using MediatR;
using TicketBox.Application.Features.Bookings.Queries;
using TicketBox.Application.Features.Bookings.Results;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Bookings.Handlers
{
    public class GetByIdBookingQueryHandler : IRequestHandler<GetByIdBookingQuery, GetByIdBookingQueryResult>
    {
        private readonly IGenericRepository<Booking> _bookingRepository;
        private readonly IMapper _mapper;

        public GetByIdBookingQueryHandler(IGenericRepository<Booking> bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdBookingQueryResult> Handle(GetByIdBookingQuery request, CancellationToken cancellationToken)
        {
          var value = await _bookingRepository.GetByIdAsync(request.BookingId);
            return _mapper.Map<GetByIdBookingQueryResult>(value);
        }
    }
}
