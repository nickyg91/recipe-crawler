using AutoMapper;
using RecipeCrawler.Entities;
using RecipeCrawler.ViewModels.ViewModels;

namespace RecipeCrawler.ViewModels.Mappers;

public class CookbookProfile : Profile
{
    public CookbookProfile()
    {
        CreateMap<CookbookViewModel, Cookbook>(MemberList.Destination)
            .ForMember(dst => dst.Id,
                x => 
                    x.MapFrom(src => src.Id))
            .ForMember(dst => dst.Name, 
                x => x
                    .MapFrom(src => src.Name))
            .ForMember(dst => dst.ChefId, x => x.Ignore())
            .ForMember(dst => dst.CoverImage, 
                x => 
                    x.Ignore())
            .ForMember(dst => dst.Recipes, 
                x => 
                    x.MapFrom(src => src.Recipes))
            .ForMember(dst => dst.Chef, 
                x => x.Ignore())
            .ForMember(dst => dst.CreatedAtUtc, 
                x => x.Ignore())
            .ReverseMap();

        CreateMap<Cookbook, CookbookViewModel>()
            .ForMember(dst => dst.CoverImageBase64,
                x => x.MapFrom(src => src.CoverImage != null ? Convert.ToBase64String(src.CoverImage) : null));

    }
}