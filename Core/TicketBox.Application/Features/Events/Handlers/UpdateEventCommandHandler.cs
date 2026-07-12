using AutoMapper;
using MediatR;
using TicketBox.Application.Features.Events.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand,Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken) //handle metodu UpdateEventCommand nesnesini alır ve veritabanındaki ilgili etkinliği günceller.
        {
            //kaydı bul
            var existingEvent = await _context.Events.FindAsync(new object[] { request.EventId },cancellationToken);

            if (existingEvent == null)
            {
                throw new Exception("Etkinlik Bulunamadı.");
            }
            //Mevcut kaydı, request'ten gelen yeni verilerle güncelle
            _mapper.Map(request, existingEvent);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}