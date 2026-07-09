using AutoMapper;
using MediatR;
using TicketBox.Application.Features.Categories.Queries;
using TicketBox.Application.Features.Categories.Results;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Categories.Handlers
{
    public class GetCategoryQueryHandler :IRequestHandler<GetCategoryQuery, List<GetCategoryQueryResult>>
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IMapper mapper, IGenericRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<GetCategoryQueryResult>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var values = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<GetCategoryQueryResult>>(values);
        }
    }
}
