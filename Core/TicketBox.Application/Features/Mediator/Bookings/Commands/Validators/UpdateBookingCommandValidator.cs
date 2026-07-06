using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Bookings.Commands.Validators
{
    public class UpdateBookingCommandValidator :AbstractValidator<UpdateBookingCommand>
    {
        private readonly TicketContext _ticketContext;

        public UpdateBookingCommandValidator(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;


            RuleFor(x => x.BookingId).NotEmpty().WithMessage("Rezervasyon seçimi zorunludur.");
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage("Tutar negatif olamaz.");

            // Güncelleme sırasında bilet sayısı değişiyorsa kapasiteyi tekrar kontrol etmeliyiz
            RuleFor(x => x)
                .MustAsync(async (cmd, ct) =>
                {
                    var existingBooking = await _ticketContext.Bookings
                        .Include(x => x.Tickets) // Bilet listesini dahil ediyoruz
                        .FirstOrDefaultAsync(x => x.BookingId == cmd.BookingId, ct); // Mevcut rezervasyonu getiriyoruz

                    if (existingBooking == null) return false; // Rezervasyon bulunamazsa false döndür

                    // Mevcut biletleri toplamdan çıkarıp yeni sayıyı ekleyerek kontrol et
                    var totalBookings = await _ticketContext.Bookings
                        .Where(x => x.EventId == existingBooking.EventId && x.BookingId != cmd.BookingId) // Aynı etkinlik için diğer rezervasyonları alıyoruz, güncellenen rezervasyonu hariç tutuyoruz
                        .SumAsync(x => x.Tickets.Count, ct);

                    return (totalBookings + cmd.NewTicketCount) <= existingBooking.Event.Capacity; // Güncelleme sonrası toplam bilet sayısı etkinlik kapasitesini aşmamalı
                })
                .WithMessage("Güncelleme sonrası etkinlik kapasitesi yetersiz kalıyor!");
        }
    }
}
