using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Mediator.Events.Queries;
using TicketBox.Application.Features.Mediator.Events.Results;
using TicketBox.Persistance.Context;

namespace TicketBox.Application.Features.Mediator.Events.Handlers
{
    public class GetByIdEventQueryHandler : IRequestHandler<GetByIdEventQuery, GetByIdEventQueryResult> //istek ve yanıt
    {
        private readonly TicketContext _ticketContext;
        private readonly IMapper _mapper;

        public GetByIdEventQueryHandler(TicketContext ticketContext, IMapper mapper)
        {
            _ticketContext = ticketContext;
            _mapper = mapper;
        }

        public async Task<GetByIdEventQueryResult> Handle(GetByIdEventQuery request, CancellationToken cancellationToken) //bu metot request.Id parametresini alır ve veritabanında bu Id'ye sahip bir etkinlik arar.
                                                                                                                          //Eğer etkinlik bulunamazsa bir InvalidOperationException fırlatır.
                                                                                                                         //Eğer etkinlik bulunursa, etkinliğin bilgilerini GetByIdEventQueryResult nesnesine dönüştürür ve geri döndürür.
        {
            var value = await _ticketContext.Events.Where(x => x.EventId == request.Id).FirstOrDefaultAsync(cancellationToken);
            
            return _mapper.Map<GetByIdEventQueryResult>(value); //AutoMapper kullanarak Event nesnesini GetByIdEventQueryResult nesnesine dönüştürür ve geri döndürür.
        }
    }
}
