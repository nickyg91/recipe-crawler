using AutoMapper;
using RecipeCrawler.Entities;
using RecipeCrawler.ViewModels.ViewModels;

namespace RecipeCrawler.ViewModels.Mappers;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        CreateMap<IngredientViewModel, Ingredient>(MemberList.Destination)
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Id))
            .ForMember(dst => dst.Measurement, x => x.MapFrom(src => src.Measurement))
            .ForMember(dst => dst.Name, x => x.MapFrom(src => src.Name))
            .ForMember(dst => dst.Amount, x => x.MapFrom(src => src.Amount))
            .ForMember(dst => dst.RecipeId, x => x.MapFrom(src => src.RecipeId))
            .ForMember(dst => dst.StepIngredients, x => x.Ignore())
            .ForMember(dst => dst.CreatedAtUtc, x => x.Ignore())
            .ReverseMap();

        CreateMap<IngredientViewModel, StepIngredient>(MemberList.Destination)
            .ForMember(dst => dst.StepId, x => x.MapFrom(src => src.StepId))
            .ForMember(dst => dst.Ingredient, x => x.MapFrom(src => src))
            .ReverseMap();
        
        CreateMap<Ingredient, IngredientViewModel>(MemberList.Destination)
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Id))
            .ForMember(dst => dst.Measurement, x => x.MapFrom(src => src.Measurement))
            .ForMember(dst => dst.Name, x => x.MapFrom(src => src.Name))
            .ForMember(dst => dst.Amount, x => x.MapFrom(src => src.Amount))
            .ForMember(dst => dst.RecipeId, x => x.MapFrom(src => src.RecipeId))
            .ReverseMap();
        
        CreateMap<StepIngredient, IngredientViewModel>(MemberList.Destination)
            .ForMember(dst => dst.StepId, x => x.MapFrom(src => src.StepId))
            .ForMember(dst => dst.Amount, x => x.MapFrom(src => src.Ingredient.Amount))
            .ForMember(dst => dst.Measurement, x => x.MapFrom(src => src.Ingredient.Measurement))
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Ingredient.Id))
            .ForMember(dst => dst.Name, x => x.MapFrom(src => src.Ingredient.Name))
            .ReverseMap();
    }
}