using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Bookings.Specifications;
using TicketBox.Application.Features.Common.Specifications;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Bookings.Commands.Validators
{
    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBookingCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.BookingId).NotEmpty().WithMessage("Rezervasyon seçimi zorunludur.");
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage("Tutar negatif olamaz.");

            // Güncelleme sırasında bilet sayısı değişiyorsa kapasite tekrar kontrol edilmeli
            RuleFor(x => x).MustAsync(async (cmd, ct) =>
              {
                  //Mevcut rezervasyonu getir
                  var existingBooking = await _context.Bookings.FindAsync(new object[] { cmd.BookingId },ct);
                  if (existingBooking == null) return false;

                  //Etkinliği getir (kapasite bilgisini almak için)
                  var eventEntity = await _context.Events.FindAsync(new object[] { existingBooking.BookingId },ct);
                  if (eventEntity == null) return false;

                  //Specificationı kullanarak diğer rezervasyonları getir
                  var spec = new BookingsByEventSpecification(existingBooking.EventId);

                  // SpecificationEvaluator ile sorguyu hazırla ve çalıştır
                  var query = _context.Bookings.AsQueryable();
                  var otherBookings = await SpecificationEvaluator<Booking>.GetQuery(query, spec).ToListAsync(ct);

                  //Bilet sayısı
                  var totalOtherTickets = otherBookings
                        .Where(x => x.BookingId != cmd.BookingId)
                        .Sum(x => x.Tickets.Count);

                  return (totalOtherTickets + cmd.NewTicketCount) <= eventEntity.Capacity;
              })
                  .WithMessage("Güncelleme sonrası etkinlik kapasitesi yetersiz kalıyor!");
        }
    }
}
