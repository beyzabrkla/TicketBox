using MediatR;
using TicketBox.Application.Features.Events.Commands;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand,Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEventCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken) //handle metodu UpdateEventCommand nesnesini alır ve veritabanındaki ilgili etkinliği günceller.
        {
            var values = await _context.Events.FindAsync(new object[] { request.EventId },cancellationToken);

            if (values == null)
            {
                throw new Exception("Etkinlik Bulunamadı!");
            }

            values.Title = request.Title;
            values.Description = request.Description;
            values.EventDate = request.EventDate;
            values.Location = request.Location;
            values.Capacity = request.Capacity;
            values.Price = request.Price;
            values.ImageUrl = request.ImageUrl;
            values.IsActive = request.IsActive;
            values.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}