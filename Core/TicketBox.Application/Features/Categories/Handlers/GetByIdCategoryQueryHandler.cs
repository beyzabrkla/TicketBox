using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketBox.Application.Features.Categories.Queries;
using TicketBox.Application.Features.Categories.Results;
using TicketBox.Domain.Entities;
using TicketBox.Domain.Interfaces;

namespace TicketBox.Application.Features.Categories.Handlers
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, GetByIdCategoryQueryResult>
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetByIdCategoryQueryHandler(IMapper mapper, IGenericRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<GetByIdCategoryQueryResult> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            return _mapper.Map<GetByIdCategoryQueryResult>(category);
        }
    }
}