using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class GetEventStatsQueryHandler : IRequestHandler<GetEventStatsQuery, EventStatsResult>
    {
        private readonly IApplicationDbContext _context;

        public GetEventStatsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EventStatsResult> Handle(GetEventStatsQuery request, CancellationToken cancellationToken)
        {
            // Mevcut ayın başlangıç ve bitiş tarihlerini hesapla (Raporlama aralığı için)
            var now = DateTime.Now;
            var monthStart = new DateTime(now.Year, now.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            // Aktif etkinlik sayısını veritabanından al
            var activeEventCount = await _context.Events
                .CountAsync(e => e.IsActive, cancellationToken);

            // Taslak durumundaki etkinlik sayısını veritabanından al
            var draftEventCount = await _context.Events
                .CountAsync(e => !e.IsActive, cancellationToken);

            // İlgili ay içerisindeki toplam satış gelirini hesapla
            var monthlyRevenue = await _context.Tickets
                .Where(t => t.IsActive
                            && t.PurchaseDate >= monthStart
                            && t.PurchaseDate < monthEnd)
                .SumAsync(t => (decimal?)t.Price, cancellationToken) ?? 0;

            // Kapasitesi dolmuş etkinliklerin sayısını hesapla
            var soldOutEventCount = await _context.Events
                .CountAsync(e => e.Tickets.Count(t => t.IsActive) >= e.Capacity, cancellationToken);

            return new EventStatsResult
            {
                ActiveEventCount = activeEventCount,
                MonthlyRevenue = monthlyRevenue,
                SoldOutEventCount = soldOutEventCount,
                DraftEventCount = draftEventCount
            };
        }
    }
}