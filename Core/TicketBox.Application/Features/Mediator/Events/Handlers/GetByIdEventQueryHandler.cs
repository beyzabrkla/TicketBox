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

        public GetByIdEventQueryHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task<GetByIdEventQueryResult> Handle(GetByIdEventQuery request, CancellationToken cancellationToken) //bu metot request.Id parametresini alır ve veritabanında bu Id'ye sahip bir etkinlik arar.
                                                                                                                          //Eğer etkinlik bulunamazsa bir InvalidOperationException fırlatır.
                                                                                                                         //Eğer etkinlik bulunursa, etkinliğin bilgilerini GetByIdEventQueryResult nesnesine dönüştürür ve geri döndürür.
        {
            var value = await _ticketContext.Events.Where(x => x.EventId == request.Id)
                .Select(x=> new GetByIdEventQueryResult //Select metodu ile veritabanından gelen Event nesnesini GetByIdEventQueryResult nesnesine dönüştürür.
            {
                EventId = x.EventId,
                Title = x.Title,
                Description = x.Description,
                EventDate = x.EventDate,
                Location = x.Location,
                Capacity = x.Capacity,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                IsActive = x.IsActive,
                CategoryId = x.CategoryId
            }).FirstOrDefaultAsync(cancellationToken);

            return value;
        }
    }
}
