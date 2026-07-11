using AutoMapper;
using MediatR;
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
            var value = await _context.Events.FindAsync(new object[] { request.Id },cancellationToken);
            if (value == null)
            {
                throw new Exception("Etkinlik bulunamadı!");
            }
            return _mapper.Map<GetByIdEventQueryResult>(value); //AutoMapper kullanarak Event nesnesini GetByIdEventQueryResult nesnesine dönüştürür ve geri döndürür.
        }
    }
}
