using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TicketBox.Application.Features.Mediator.Bookings.Queries;
using TicketBox.Application.Features.Mediator.Bookings.Results;
using TicketBox.Application.Features.Mediator.Bookings.Specifications;

namespace TicketBox.Application.Features.Mediator.Bookings.Handlers
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, List<GetBookingQueryResult>>
    {
        private readonly TicketContext _ticketContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetBookingQueryHandler(TicketContext ticketContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ticketContext = ticketContext;
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

            // Evaluator üzerinden sorguyu çalıştır
            var query = SpecificationEvaluator<Booking>.GetQuery(_ticketContext.Bookings.AsQueryable(), spec);

            var values = await query.ToListAsync(cancellationToken);

            return _mapper.Map<List<GetBookingQueryResult>>(values);
        }
    }
}
