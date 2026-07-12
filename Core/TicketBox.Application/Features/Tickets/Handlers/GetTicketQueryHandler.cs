using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Common.Specifications;
using TicketBox.Application.Features.Tickets.Queries;
using TicketBox.Application.Features.Tickets.Results;
using TicketBox.Application.Features.Tickets.Specifications;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Tickets.Handlers
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, List<GetTicketQueryResult>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTicketQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GetTicketQueryResult>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            //Specificationı oluştur 
            var spec = new UserTicketsSpecification(request.UserId);

            //Evaluator ile sorguyu oluştur
            var query = SpecificationEvaluator<Ticket>.GetQuery(_context.Tickets.AsQueryable(), spec);

            //Veritabanından veriyi çek
            var values = await query.ToListAsync(cancellationToken);

            return _mapper.Map<List<GetTicketQueryResult>>(values);
        }
    }
}
