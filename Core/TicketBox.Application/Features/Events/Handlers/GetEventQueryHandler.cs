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

        public async Task<List<GetEventQueryResult>> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            // Artık FilterEventsSpecification kullanıyoruz
            // request içindeki değerleri buraya aktarıyoruz
            var spec = new FilterEventsSpecification(
                request.CategoryIds,
                true,                // isActive
                null,                // minPrice
                request.MaxPrice,    // maxPrice
                false,               // upcoming
                false,               // soldOut
                request.SearchTerm,
                request.Page > 0 ? request.Page : 1, // Sayfa numarası kontrolü
                    6
            );

            //Sorguyu başlat ve Specification'ı uygula
            var query = _context.Events.AsQueryable();

            // SpecificationEvaluator ile veritabanı sorgusunu oluşturuyoruz
            var values = await SpecificationEvaluator<Event>.GetQuery(query, spec).ToListAsync(cancellationToken);

            //AutoMapper ile dönüştürüp döndür
            return _mapper.Map<List<GetEventQueryResult>>(values);
        }
    }
}
