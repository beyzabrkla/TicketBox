using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TicketBox.Application.Features.Bookings.Queries;
using TicketBox.Application.Features.Bookings.Results;
using TicketBox.Application.Features.Bookings.Specifications;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Bookings.Handlers
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, List<GetBookingQueryResult>>
    {
        private readonly IGenericRepository<Booking> _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetBookingQueryHandler(IGenericRepository<Booking> bookingRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GetBookingQueryResult>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("Kullanıcı girişi bulunamadı!");

            //Specification
            var spec = new BookingsByUserSpecification(userId);

            var values = await _bookingRepository.ListAsync(spec, cancellationToken);

            return _mapper.Map<List<GetBookingQueryResult>>(values);
        }
    }
}
