using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Common.Specifications;
using TicketBox.Application.Features.Users.Queries;
using TicketBox.Application.Features.Users.Results;
using TicketBox.Application.Features.Users.Specifications;
using TicketBox.Application.Interfaces;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Features.Users.Handlers
{
    public class GetUsersWithTicketsQueryHandler : IRequestHandler<GetUsersWithTicketsQuery, List<UserWithTicketsResult>>
    {
        private readonly IApplicationDbContext _context;

        public GetUsersWithTicketsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserWithTicketsResult>> Handle(GetUsersWithTicketsQuery request, CancellationToken cancellationToken)
        {
            //Specification'ı tanımla
            var spec = new GetUsersWithTicketsSpecification();

            //Query'yi al ve Specification ile filtrele
            var query = _context.Users.AsQueryable();
            var filteredQuery = SpecificationEvaluator<ApplicationUser>.GetQuery(query, spec);

            //Select ile sonucu DTO'ya çevir
            return await filteredQuery
                .Select(u => new UserWithTicketsResult
                {
                    Id = u.Id,
                    FullName = u.Name + " " + u.Surname,
                    Email = u.Email,
                    TicketCount = u.Tickets.Count
                })
                .ToListAsync(cancellationToken);
        }
    }
}