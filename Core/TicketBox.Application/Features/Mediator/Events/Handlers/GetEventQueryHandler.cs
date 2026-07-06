using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Mediator.Events.Queries;
using TicketBox.Application.Features.Mediator.Events.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, List<GetEventQueryResult>> //IRequestHandler arayüzünü uygular ve GetEventQuery isteğini işlemek için kullanılır. Bu sınıf, GetEventQuery isteğini alır ve bir liste döndürür.
    {                                                  //controllerda istek gönderildiğinde, bu handler çağrılır, veritabanından etkinlikleri alır ve ikinci parametre olarak sonuçları döndürür.     
        private readonly TicketContext _ticketContext;
        private readonly IMapper _mapper;

        public GetEventQueryHandler(TicketContext ticketContext, IMapper mapper)
        {
            _ticketContext = ticketContext;
            _mapper = mapper;
        }

        public async Task<List<GetEventQueryResult>> Handle(GetEventQuery request, CancellationToken cancellationToken) //bu metot , GetEventQuery isteğini işlemek için çağrılır ve bir liste döndürür ve CancellationToken parametresi ile iptal edilebilir.
        {
            var values = await _ticketContext.Events.ToListAsync(cancellationToken);

            return _mapper.Map<List<GetEventQueryResult>>(values);
        }
    }
}
