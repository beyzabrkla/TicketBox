using MediatR;
using TicketBox.Application.Features.Events.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
    {
        private readonly IGenericRepository<Event> _eventRepository;

        public CreateEventCommandHandler(IGenericRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
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
                ImageUrl = request.ImageUrl,
                IsActive = request.IsActive,
                CategoryId = request.CategoryId
            };
            await _eventRepository.AddAsync(values);
            await _eventRepository.SaveChangesAsync();
        }
    }
}
