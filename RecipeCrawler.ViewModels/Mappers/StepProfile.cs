using AutoMapper;
using RecipeCrawler.Entities;
using RecipeCrawler.ViewModels.ViewModels;

namespace RecipeCrawler.ViewModels.Mappers;

public class StepProfile : Profile
{
    public StepProfile()
    {
        CreateMap<StepViewModel, Step>(MemberList.Destination)
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Id))
            .ForMember(dst => dst.Description, x => x.MapFrom(src => src.Description))
            .ForMember(dst => dst.RecipeId, x => x.MapFrom(src => src.RecipeId))
            .ForMember(dst => dst.Recipe, x => x.Ignore())
            .ForMember(dst => dst.CreatedAtUtc, x => x.Ignore())
            .ReverseMap();
    }
}