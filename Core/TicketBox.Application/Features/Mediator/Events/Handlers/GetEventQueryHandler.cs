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

        public GetEventQueryHandler(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }

        public async Task<List<GetEventQueryResult>> Handle(GetEventQuery request, CancellationToken cancellationToken) //bu metot , GetEventQuery isteğini işlemek için çağrılır ve bir liste döndürür ve CancellationToken parametresi ile iptal edilebilir.
        {
            var values = await _ticketContext.Events.Select(x => new GetEventQueryResult
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
            }).ToListAsync(cancellationToken);
            
            return values;
        }
    }
}
