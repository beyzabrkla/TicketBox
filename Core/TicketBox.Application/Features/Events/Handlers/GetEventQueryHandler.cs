using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Common.Specifications;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Application.Features.Events.Specifications;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, List<GetEventQueryResult>> //IRequestHandler arayüzünü uygular ve GetEventQuery isteğini işlemek için kullanılır. Bu sınıf, GetEventQuery isteğini alır ve bir liste döndürür.
    {                                                  //controllerda istek gönderildiğinde, bu handler çağrılır, veritabanından etkinlikleri alır ve ikinci parametre olarak sonuçları döndürür.     
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEventQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GetEventQueryResult>> Handle(GetEventQuery request, CancellationToken cancellationToken) //bu metot , GetEventQuery isteğini işlemek için çağrılır ve bir liste döndürür ve CancellationToken parametresi ile iptal edilebilir.
        {
            var spec = new EventListSpecification();

            //sorguyu başlat
            var query = _context.Events.AsQueryable();

            var values = await SpecificationEvaluator<Event>.GetQuery(query, spec).ToListAsync(); // EventListSpecification içerisinde Category ve Tickets tabloları Include edildiğinden Entity Framework ilgili ilişkili verileri de tek sorguda yükler.
                                                                

            return _mapper.Map<List<GetEventQueryResult>>(values);  // Daha sonra AutoMapper ile Event nesneleri GetEventQueryResult nesnelerine dönüştürülerek Controller'a geri gönderilir.
        }
    }
}
