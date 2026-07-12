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
        //Validasyonlar
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? throw new UnauthorizedAccessException("Giriş yapmalısın!");

        var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken)
                          ?? throw new Exception("Etkinlik bulunamadı.");

        var soldTicketsCount = await _context.Tickets.CountAsync(t => t.EventId == request.EventId, cancellationToken);
        if (soldTicketsCount + request.TicketCount > eventEntity.Capacity)
            throw new Exception("Yeterli kontenjan yok!");

        //(Transaction)
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            // Biletleri oluştur
            var booking = _mapper.Map<Booking>(request);
            booking.AppUserId = userId;
            booking.BookingDate = DateTime.UtcNow;
            booking.ServiceFee = 150;
            booking.TotalAmount = ((eventEntity.Price ?? 0) * request.TicketCount) + booking.ServiceFee;

            int lastTicketCount = await _context.Tickets.CountAsync(cancellationToken);
            booking.AddTickets(request.TicketCount, request.EventId, userId, eventEntity.Price ?? 0, lastTicketCount);

            // Kapasiteyi düşür
            eventEntity.Capacity -= request.TicketCount;
            _context.Events.Update(eventEntity);

            await _context.Bookings.AddAsync(booking, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            // Cache'i temizle! 
            // "EventDetail_" ön eki, GetByIdEventQueryHandler içerisinde cache'i oluştururken kullandığın anahtarla AYNI olmalı.
            _cache.Remove($"EventDetail_{request.EventId}");

            //Başarılı olduktan sonra maili tetikle
            await _mediator.Publish(new BookingCreatedEvent(booking, eventEntity.Title, userId), cancellationToken);

            return booking.BookingId;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}