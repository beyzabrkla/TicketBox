using FluentValidation;
using TicketBox.Application.Features.Bookings.Specifications;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Bookings.Commands.Validators
{
    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
        private readonly IGenericRepository<Booking> _bookingRepository;
        private readonly IGenericRepository<Event> _eventRepository;

        public UpdateBookingCommandValidator(IGenericRepository<Booking> bookingRepository, IGenericRepository<Event> eventRepository)
        {
            _bookingRepository = bookingRepository;
            _eventRepository = eventRepository;


            RuleFor(x => x.BookingId).NotEmpty().WithMessage("Rezervasyon seçimi zorunludur.");
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage("Tutar negatif olamaz.");

            // Güncelleme sırasında bilet sayısı değişiyorsa kapasite tekrar kontrol edilmeli
            RuleFor(x => x).MustAsync(async (cmd, ct) =>
              {
                  //Mevcut rezervasyonu getir
                  var existingBooking = await _bookingRepository.GetByIdAsync(cmd.BookingId);
                  if (existingBooking == null) return false;

                  //Etkinliği getir (kapasite bilgisini almak için)
                  var eventEntity = await _eventRepository.GetByIdAsync(existingBooking.EventId);

                  //Diğer tüm rezervasyonları Specification ile çek
                  var otherBookings = await _bookingRepository.ListAsync(new BookingsByEventSpecification(existingBooking.EventId), ct);
                  var totalOtherTickets = otherBookings
                        .Where(x => x.BookingId != cmd.BookingId)
                        .Sum(x => x.Tickets.Count);

                  return (totalOtherTickets + cmd.NewTicketCount) <= eventEntity.Capacity;
              })
                  .WithMessage("Güncelleme sonrası etkinlik kapasitesi yetersiz kalıyor!");
        }
    }
}
