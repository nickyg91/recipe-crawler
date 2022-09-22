using AutoMapper;
using RecipeCrawler.Entities;
using RecipeCrawler.Entities.Models;

namespace RecipeCrawler.Core.Mappers.Profiles
{
    public class ChefProfile : Profile
    {
        public ChefProfile()
        {
            CreateMap<CreateAccountModel, Chef>(MemberList.None)
                .ForMember(chef => chef.PasswordHash, x => x.MapFrom(model => model.Password))
                .ForMember(chef => chef.Email, x => x.MapFrom(model => model.Email))
                .ForMember(chef => chef.Username, x => x.MapFrom(model => model.Username));
        }
    }
}
