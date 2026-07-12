using AutoMapper;
using MediatR;
using TicketBox.Application.Features.Events.Commands;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand,Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateEventCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Handle metodu, CreateEventCommand isteğini işlemek için kullanılır
        public async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken) //sayfa kapandığında işlem iptal olsun 
         {
            var newEvent = _mapper.Map<Event>(request);
            await _context.Events.AddAsync(newEvent, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
