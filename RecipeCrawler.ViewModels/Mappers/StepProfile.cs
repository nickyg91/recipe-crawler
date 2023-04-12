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
            .ForMember(dst => dst.Order, x => x.MapFrom(src => src.Order))
            .ForMember(dst => dst.Recipe, x => x.Ignore())
            .ForMember(dst => dst.StepIngredients, 
                x => x.MapFrom(src => src.Ingredients != null ? src.Ingredients.Select(y => new StepIngredient
            {
                Id = y.Id,
                StepId = y.StepId,
                Ingredient = new Ingredient
                {
                    Id = y.Id,
                    Amount = y.Amount,
                    Measurement = y.Measurement,
                    Name = y.Name,
                    RecipeId = src.RecipeId
                }
            }) : new List<StepIngredient>()))
            .ForMember(dst => dst.CreatedAtUtc, x => x.Ignore())
            .ReverseMap();
        
        CreateMap<Step, StepViewModel>(MemberList.Destination)
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Id))
            .ForMember(dst => dst.Description, x => x.MapFrom(src => src.Description))
            .ForMember(dst => dst.Order, x => x.MapFrom(src => src.Order))
            .ForMember(dst => dst.Ingredients, x => x.MapFrom(src => src.StepIngredients.Select(y => y.Ingredient)));
    }
}