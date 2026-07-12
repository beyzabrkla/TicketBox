using AutoMapper;
using TicketBox.Application.Features.Categories.Commands;
using TicketBox.Application.Features.Categories.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryQueryResult>()
                .ForMember(dest => dest.EventCount,
                           opt => opt.MapFrom(src => src.Events != null ? src.Events.Count : 0));

            CreateMap<Category, GetByIdCategoryQueryResult>();

            CreateMap<CreateCategoryCommand, Category>();

            CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}