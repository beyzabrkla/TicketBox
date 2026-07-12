using MediatR;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Bookings.Events
{
    public class BookingCreatedEvent : INotification
    {
        public Booking Booking { get; }
        public string EventTitle { get; }
        public string AppUserId { get; }
        public BookingCreatedEvent(Booking booking, string eventTitle, string appUserId)
        {
            Booking = booking;
            EventTitle = eventTitle;
            AppUserId = appUserId;
        }
    }
}
