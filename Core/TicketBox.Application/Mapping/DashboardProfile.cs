using AutoMapper;
using TicketBox.Application.Features.Dashboard.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class DashboardProfile :Profile
    {
        public DashboardProfile()
        {
            CreateMap<Booking, RecentTransactionItem>()
                .ConstructUsing(src => new RecentTransactionItem(
                    $"{src.AppUser.Name} {src.AppUser.Surname}",
                    src.Event.Title,
                    "Tamamlandı",
                    src.BookingDate,
                    src.EventId.ToString()
                ));
        }
    }
}
