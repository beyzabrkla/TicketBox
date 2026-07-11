using MediatR;
using TicketBox.Application.Features.Events.Commands;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand,Unit>
    {
        private readonly IApplicationDbContext _context;

        public CreateEventCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        // Handle metodu, CreateEventCommand isteğini işlemek için kullanılır
        public async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken) //sayfa kapandığında işlem iptal olsun 
         {
            var newEvent = new Event //Event nesnesi oluşturuluyor ve request'ten gelen verilerle dolduruluyor
            {
                Title = request.Title,
                Description = request.Description,
                EventDate = request.EventDate,
                Location = request.Location,
                Capacity = request.Capacity ?? 0,
                Price = request.Price ?? 0,
                ImageUrl = request.ImageUrl,
                IsActive = request.IsActive,
                CategoryId = request.CategoryId
            };
            await _context.Events.AddAsync(newEvent, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
