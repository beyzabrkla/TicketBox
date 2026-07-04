using MediatR;
using TicketBox.Application.Features.Mediator.Events.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
    {
        private readonly TicketContext _ticketContext;

        public CreateEventCommandHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        // Handle metodu, CreateEventCommand isteğini işlemek için kullanılır
        public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken) //sayfa kapandığında işlem iptal olsun 
         {
            var values = new Event //Event nesnesi oluşturuluyor ve request'ten gelen verilerle dolduruluyor
            {
                Title = request.Title,
                Description = request.Description,
                EventDate = request.EventDate,
                Location = request.Location,
                Capacity = request.Capacity,
                Price = request.Price,
                ImageUrl = request.ImageUrl
            };
            await _ticketContext.Events.AddAsync(values);
            await _ticketContext.SaveChangesAsync();
        }
    }
}
