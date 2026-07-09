using AutoMapper;
using MediatR;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, List<GetEventQueryResult>> //IRequestHandler arayüzünü uygular ve GetEventQuery isteğini işlemek için kullanılır. Bu sınıf, GetEventQuery isteğini alır ve bir liste döndürür.
    {                                                  //controllerda istek gönderildiğinde, bu handler çağrılır, veritabanından etkinlikleri alır ve ikinci parametre olarak sonuçları döndürür.     
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public GetEventQueryHandler(IGenericRepository<Event> eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<List<GetEventQueryResult>> Handle(GetEventQuery request, CancellationToken cancellationToken) //bu metot , GetEventQuery isteğini işlemek için çağrılır ve bir liste döndürür ve CancellationToken parametresi ile iptal edilebilir.
        {
            var values = await _eventRepository.GetAllAsync();

            return _mapper.Map<List<GetEventQueryResult>>(values);
        }
    }
}
