using AutoMapper;
using AutoMapper.EquivalencyExpression;
using RecipeCrawler.Entities;
using RecipeCrawler.ViewModels.ViewModels;

namespace RecipeCrawler.ViewModels.Mappers;

public class StepProfile : Profile
{
    public StepProfile()
    {
        CreateMap<StepViewModel, Step>(MemberList.Destination)
            .EqualityComparison((m, e) => m.Id == e.Id)
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Id))
            .ForMember(dst => dst.Description, x => x.MapFrom(src => src.Description))
            .ForMember(dst => dst.Order, x => x.MapFrom(src => src.Order))
            .ForMember(dst => dst.Recipe, x => x.Ignore())
            .ForMember(dst => dst.Ingredients, x => x.MapFrom(src => src.Ingredients))
            .ForMember(dst => dst.CreatedAtUtc, x => x.Ignore())
            .ReverseMap();
        
        CreateMap<Step, StepViewModel>(MemberList.Destination)
            .EqualityComparison((e, m) => e.Id == m.Id)
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Id))
            .ForMember(dst => dst.Description, x => x.MapFrom(src => src.Description))
            .ForMember(dst => dst.Order, x => x.MapFrom(src => src.Order))
            .ForMember(dst => dst.Ingredients, x => x.MapFrom(src => src.Ingredients))
            .ReverseMap();
    }
}