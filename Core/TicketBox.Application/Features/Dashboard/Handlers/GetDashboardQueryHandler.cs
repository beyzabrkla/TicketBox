using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Dashboard.Queries;
using TicketBox.Application.Features.Dashboard.Results;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Dashboard.Handlers
{
    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardQueryResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDashboardQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DashboardQueryResult> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            //Toplam Satış Hizmet bedeli hariç
            var totalSales = await _context.Bookings
                .Where(b => b.Event != null)
                .SumAsync(b => b.TotalAmount - b.ServiceFee, cancellationToken);

            //Kategori bazlı satış dağılımı
            var categorySales = await _context.Bookings
                .Where(b => b.Event != null)
                .Include(b => b.Event)
                .ThenInclude(e => e.Category)
                .GroupBy(b => b.Event.Category.CategoryName)
                .Select(g => new CategorySalesResult(
                    g.Key,
                    totalSales > 0 ? (g.Sum(b => b.TotalAmount) / totalSales) * 100 : 0
                ))
                .ToListAsync(cancellationToken);

            //Son 5 işlem
            var recentTransactions = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.AppUser)
                .OrderByDescending(x => x.BookingDate)
                .Take(5)
                .ProjectTo<RecentTransactionItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            //Aktif etkinlik ve kullanıcı sayıları
            var activeEvents = await _context.Events.CountAsync(e => e.IsActive, cancellationToken);
            var userCount = await _context.Users.CountAsync(cancellationToken);

            //kapasite hesapla
            var totalCapacity = await _context.Events.SumAsync(e => e.Capacity ?? 0, cancellationToken);
            var totalSoldTickets = await _context.Tickets.CountAsync(t => t.IsActive, cancellationToken);

            int capacityUsagePercentage = totalCapacity > 0
                ? (int)((double)totalSoldTickets / totalCapacity * 100)
                : 0;

            return new DashboardQueryResult(
                TotalGrossSales: totalSales,
                ActiveEventsCount: activeEvents,
                CapacityUsagePercentage: capacityUsagePercentage,
                NewUsersCount: userCount,
                RecentTransactions: recentTransactions,
                CategorySales: categorySales
            );
        }
    }
}