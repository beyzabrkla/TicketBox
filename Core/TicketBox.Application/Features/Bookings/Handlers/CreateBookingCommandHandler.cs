using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using TicketBox.Application.Features.Bookings.Commands;
using TicketBox.Application.Features.Bookings.Events;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMemoryCache _cache;

    public CreateBookingCommandHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IMediator mediator,
        IHttpContextAccessor httpContextAccessor,
        IMemoryCache cache)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
        _cache = cache;
    }

    public async Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        // Kullanıcı kimlik doğrulamasını kontrol et
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? throw new UnauthorizedAccessException("Giriş yapmalısın!");

        // İlgili etkinlik sistemde var mı
        var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken)
                          ?? throw new Exception("Etkinlik bulunamadı.");

        // Seçilen bilet adediyle toplam kapasiteyi kontrol et
        var soldTicketsCount = await _context.Tickets.CountAsync(t => t.EventId == request.EventId, cancellationToken);
        if (soldTicketsCount + request.TicketCount > eventEntity.Capacity)
            throw new Exception("Yeterli kontenjan yok!");

        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            // Rezervasyon nesnesini oluştur ve temel bilgileri ata
            var booking = _mapper.Map<Booking>(request);
            booking.AppUserId = userId;
            booking.BookingDate = DateTime.UtcNow;
            booking.ServiceFee = 150; // hizmet bedeli

            // Toplam tutarı ve bilet sayısını Booking üzerinden hesapla
            booking.TicketCount = request.TicketCount;
            booking.TotalAmount = (eventEntity.Price ?? 0) * request.TicketCount + booking.ServiceFee; //toplam ücret

            // Biletleri Booking sınıfındaki metot ile üret
            int lastTicketCount = await _context.Tickets.CountAsync(cancellationToken);
            booking.AddTickets(request.TicketCount, request.EventId, userId, eventEntity.Price ?? 0, lastTicketCount);

            // Rezervasyonu kaydet
            await _context.Bookings.AddAsync(booking, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            // Güncel etkinlik bilgisini yansıtmak için cache'i temizle
            _cache.Remove($"EventDetail_{request.EventId}");

            // Rezervasyon başarıyla tamamlandığında bilgilendirme mailini tetikle
            await _mediator.Publish(new BookingCreatedEvent(booking, eventEntity.Title, userId), cancellationToken);

            return booking.BookingId;
        }
        catch
        {
            // Hata durumunda tüm değişiklikleri geri al
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}