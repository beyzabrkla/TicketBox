using AutoMapper;
using MediatR;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class GetByIdEventQueryHandler : IRequestHandler<GetByIdEventQuery, GetByIdEventQueryResult> //istek ve yanıt
    {
        private readonly IGenericRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public GetByIdEventQueryHandler(IGenericRepository<Event> eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdEventQueryResult> Handle(GetByIdEventQuery request, CancellationToken cancellationToken) //bu metot request.Id parametresini alır ve veritabanında bu Id'ye sahip bir etkinlik arar.
                                                                                                                          //Eğer etkinlik bulunamazsa bir InvalidOperationException fırlatır.
                                                                                                                         //Eğer etkinlik bulunursa, etkinliğin bilgilerini GetByIdEventQueryResult nesnesine dönüştürür ve geri döndürür.
        {
            var value = await _eventRepository.GetByIdAsync(request.Id);
            
            return _mapper.Map<GetByIdEventQueryResult>(value); //AutoMapper kullanarak Event nesnesini GetByIdEventQueryResult nesnesine dönüştürür ve geri döndürür.
        }
    }
}
