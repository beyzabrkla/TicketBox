using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class GetByIdEventQueryHandler : IRequestHandler<GetByIdEventQuery, GetByIdEventQueryResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GetByIdEventQueryHandler(IMapper mapper, IApplicationDbContext context, IMemoryCache cache)
        {
            _mapper = mapper;
            _context = context;
            _cache = cache; 
        }

        public async Task<GetByIdEventQueryResult> Handle(GetByIdEventQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"EventDetail_{request.Id}"; //Anahtar: Booking handler'daki ile aynı

            //Cachete var mı 
            if (_cache.TryGetValue(cacheKey, out GetByIdEventQueryResult cachedResult))
            {
                return cachedResult;
            }

            //Cachete yoksa veritabanına git
            var value = await _context.Events
                    .Include(e => e.Tickets)
                    .Include(e => e.Category)
                    .FirstOrDefaultAsync(x => x.EventId == request.Id, cancellationToken);

            if (value == null)
            {
                throw new Exception("Etkinlik bulunamadı!");
            }

            var result = _mapper.Map<GetByIdEventQueryResult>(value);
            result.ServiceFee = 150;

            // aktif biletleri sayıyoruz
            var activeSoldCount = value.Tickets?.Count(t => t.IsActive) ?? 0;
            result.SoldTicketCount = activeSoldCount;

            //Kalan koltuk
            result.SoldTicketCount = value.Tickets?.Count(t => t.IsActive) ?? 0;

            //Veritabanından gelen sonucu Cache'e kaydet
            _cache.Set(cacheKey, result, TimeSpan.FromHours(1));

            return result;
        }
    }
}