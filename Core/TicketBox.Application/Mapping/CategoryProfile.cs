using AutoMapper;
using TicketBox.Application.Features.Categories.Results;
using TicketBox.Domain.Entities;

namespace TicketBox.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryQueryResult>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.IconName, opt => opt.MapFrom(src => src.IconName))
                .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.IconUrl))
                // Count alanı burada dolduruluyor:
                .ForMember(dest => dest.EventCount, opt => opt.MapFrom(src => src.Events != null ? src.Events.Count : 0))
                .ReverseMap();

            CreateMap<Category, GetByIdCategoryQueryResult>().ReverseMap();
        }
    }
}