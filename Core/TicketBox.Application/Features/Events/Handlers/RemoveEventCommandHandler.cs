using MediatR;
using TicketBox.Application.Features.Events.Commands;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class RemoveEventCommandHandler : IRequestHandler<RemoveEventCommand>
    {
        private readonly IGenericRepository<Event> _eventRepository;

        public RemoveEventCommandHandler(IGenericRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task Handle(RemoveEventCommand request, CancellationToken cancellationToken)
        {
            var values = await _eventRepository.GetByIdAsync(request.EventId);
            if (values != null)
            {
                await _eventRepository.RemoveAsync(values);
                await _eventRepository.SaveChangesAsync();
            }
        }
    }
}
