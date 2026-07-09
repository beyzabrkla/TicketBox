using MediatR;
using TicketBox.Application.Features.Events.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IGenericRepository<Event> _eventRepository;

        public UpdateEventCommandHandler(IGenericRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken) //handle metodu UpdateEventCommand nesnesini alır ve veritabanındaki ilgili etkinliği günceller.
        {
            var values = await _eventRepository.GetByIdAsync(request.EventId);

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

            await _eventRepository.SaveChangesAsync();
        }
    }
}