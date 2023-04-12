using AutoMapper;
using RecipeCrawler.Entities;
using RecipeCrawler.ViewModels.ViewModels;

namespace RecipeCrawler.ViewModels.Mappers;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<RecipeViewModel, Recipe>(MemberList.Destination)
            .ForMember(dst => dst.Id,
                x => x.MapFrom(src => src.Id))
            .ForMember(dst => dst.Name,
                x => x.MapFrom(src => src.Name))
            .ForMember(dst => dst.CrawledHtml,
                x => x.MapFrom(src => src.CrawledHtml))
            .ForMember(dst => dst.Cookbook, x => x.Ignore())
            .ForMember(dst => dst.CookbookId, x => x.MapFrom(src => src.CookbookId))
            .ForMember(dst => dst.Steps, x => x.MapFrom(src => src.Steps))
            .ForMember(dst => dst.Ingredients, x => x.MapFrom(src => src.Ingredients))
            .ForMember(dst => dst.CreatedAtUtc, x => x.Ignore())
            .ReverseMap();

    }
}