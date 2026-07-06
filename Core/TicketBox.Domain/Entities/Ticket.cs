namespace TicketBox.Domain.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string PNR { get; set; } 
        public string TicketCode { get; set; }
        public decimal Price { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public string AppUserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsActive { get; set; } //Bilet iptal edildi mi?
        public bool IsUsed { get; set; } //Bilet etkinliğe girişte kullanıldı mı?
    }
}
