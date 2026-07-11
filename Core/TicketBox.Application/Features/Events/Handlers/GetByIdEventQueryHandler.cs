using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Events.Queries;
using TicketBox.Application.Features.Events.Results;
using TicketBox.Application.Interfaces;

namespace TicketBox.Application.Features.Events.Handlers
{
    public class GetByIdEventQueryHandler : IRequestHandler<GetByIdEventQuery, GetByIdEventQueryResult> //istek ve yanıt
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdEventQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetByIdEventQueryResult> Handle(GetByIdEventQuery request, CancellationToken cancellationToken) //bu metot request.Id parametresini alır ve veritabanında bu Id'ye sahip bir etkinlik arar.
                                                                                                                          //Eğer etkinlik bulunamazsa bir InvalidOperationException fırlatır.
                                                                                                                         //Eğer etkinlik bulunursa, etkinliğin bilgilerini GetByIdEventQueryResult nesnesine dönüştürür ve geri döndürür.
        {
            var value = await _context.Events
                    .Include(e => e.Tickets)
                    .Include(e => e.Category)
                    .FirstOrDefaultAsync(x => x.EventId == request.Id, cancellationToken);

            if (value == null)
            {
                throw new Exception("Etkinlik bulunamadı!");
            }

            var result = _mapper.Map<GetByIdEventQueryResult>(value);

            // Satılan bilet sayısını manuel set ediyoruz
            result.SoldTicketCount = value.Tickets?.Count ?? 0;

            return result;
        }
    }
}
