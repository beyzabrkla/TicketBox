using AutoMapper;
using TicketBox.Application.Features.CQRS.Categories.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryQueryResult>().ReverseMap();
            CreateMap<Category, GetByIdCategoryQueryResult>().ReverseMap();
        }
    }
}
