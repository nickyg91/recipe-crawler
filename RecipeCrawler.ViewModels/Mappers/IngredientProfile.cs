using AutoMapper;
using AutoMapper.EquivalencyExpression;
using RecipeCrawler.Entities;
using RecipeCrawler.ViewModels.ViewModels;

namespace RecipeCrawler.ViewModels.Mappers;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        CreateMap<IngredientViewModel, Ingredient>(MemberList.Destination)
            .EqualityComparison((m, e) => m.Id == e.Id)
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Id))
            .ForMember(dst => dst.Measurement, x => x.MapFrom(src => src.Measurement))
            .ForMember(dst => dst.Name, x => x.MapFrom(src => src.Name))
            .ForMember(dst => dst.Amount, x => x.MapFrom(src => src.Amount))
            .ForMember(dst => dst.CreatedAtUtc, x => x.Ignore())
            .ReverseMap();

        CreateMap<Ingredient, IngredientViewModel>(MemberList.Destination)
            .EqualityComparison((e, m) => m.Id == e.Id)
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Id))
            .ForMember(dst => dst.Measurement, x => x.MapFrom(src => src.Measurement))
            .ForMember(dst => dst.Name, x => x.MapFrom(src => src.Name))
            .ForMember(dst => dst.Amount, x => x.MapFrom(src => src.Amount))
            .ReverseMap();
    }
}