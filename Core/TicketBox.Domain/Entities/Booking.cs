namespace TicketBox.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }

        public ApplicationUser AppUser { get; set; }
        public string AppUserId { get; set; }

        public DateTime BookingDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal ServiceFee { get; set; }

        public int TicketCount { get; set; } 

        public int EventId { get; set; }
        public Event Event { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        public void AddTickets(int count, int eventId, string userId, decimal unitPrice, int lastTicketCount)
        {
            for (int i = 0; i < count; i++)
            {
                //bilet oluşturma
                this.Tickets.Add(new Ticket
                {
                    Booking = this, 
                    EventId = eventId,
                    AppUserId = userId,
                    Price = unitPrice,
                    PurchaseDate = DateTime.UtcNow,
                    PNR = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper(),
                    TicketCode = $"TCK-{DateTime.Now.Year}-{(++lastTicketCount).ToString("D6")}",
                    IsActive = true,
                    IsUsed = false,
                    Quantity = 1
                });
            }
        }
    }
}
