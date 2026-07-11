namespace TicketBox.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string AppUserId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ServiceFee { get; set; } 
        public int EventId { get; set; }
        public Event Event { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
