using TicketBox.Application.Features.Events.Commands;
using TicketBox.Application.Features.Events.Results;

namespace TicketBox.Application.Features.Events.ViewModels
{
    public class EventListViewModel
    {
        public List<GetEventQueryResult> Events { get; set; }

        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // Yeni etkinlik ekleme komutu
        public CreateEventCommand CreateEventCommand { get; set; } = new();

        // Sayfa sayısını hesaplayan yardımcı özellik
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
