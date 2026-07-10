using AutoMapper;
using MediatR;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Application.Features.Events.Specifications;
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
            var values = await _eventRepository.ListAsync(
                new EventListSpecification(), cancellationToken); // EventListSpecification içerisinde Category ve Tickets tabloları Include edildiğinden Entity Framework ilgili ilişkili verileri de tek sorguda yükler.
                                                                  // Daha sonra AutoMapper ile Event nesneleri GetEventQueryResult nesnelerine dönüştürülerek Controller'a geri gönderilir.

            return _mapper.Map<List<GetEventQueryResult>>(values);
        }
    }
}
