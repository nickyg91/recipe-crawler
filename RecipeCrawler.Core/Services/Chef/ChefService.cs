using AutoMapper;
using RecipeCrawler.Core.Exceptions;
using RecipeCrawler.Core.Services.Chef.Interfaces;
using RecipeCrawler.Data.Repositories;
using RecipeCrawler.Entities;
using RecipeCrawler.ViewModels.ViewModels;

namespace RecipeCrawler.Core.Services.Chef;

public class ChefService : IChefService
{
    private readonly ICookbookRepository _cookbookRepository;
    private readonly IMapper _mapper;
    public ChefService(ICookbookRepository cookbookRepository, IMapper mapper)
    {
        _cookbookRepository = cookbookRepository;
        _mapper = mapper;
    }
    public List<CookbookViewModel> GetCookbooksForChef(int chefId)
    {
        var cookbooks = _cookbookRepository.GetCookbooksForChef(chefId).ToList();
        
        return cookbooks.Any() 
            ? _mapper.Map<List<Cookbook>, List<CookbookViewModel>>(cookbooks) 
            : new List<CookbookViewModel>();
    }

    public async Task<bool> DeleteCookbook(int chefId, int cookbookId)
    {
        var cookbook = await _cookbookRepository.GetCookbookById(cookbookId);

        if (cookbook != null && cookbook.ChefId != chefId)
        {
            throw new ChefCookbookAccessViolationException("This chef does not have access to this cookbook.");
        }

        var result = await _cookbookRepository.DeleteCookbook(cookbookId, chefId);
        return result > 0;
    }

    public async Task<bool> UpdateCookbook(CookbookViewModel cookbook, int chefId)
    {
        var dbCookbook = await _cookbookRepository.GetCookbookById(cookbook.Id);

        if (dbCookbook == null)
        {
            throw new CookbookNotFoundException("Cookbook not found!");
        }
        
        if (dbCookbook != null && dbCookbook.ChefId != chefId)
        {
            throw new ChefCookbookAccessViolationException("This chef does not have access to this cookbook.");
        }

        _mapper.Map(cookbook, dbCookbook);
        
        var results = await _cookbookRepository.UpdateCookbook(dbCookbook!);
        
        return results > 0;
    }

    public async Task<CookbookViewModel?> GetCookbookById(int cookbookId, int chefId)
    {
        var cookbook = await _cookbookRepository.GetCookbookById(cookbookId);

        if (cookbook == null)
        {
            throw new CookbookNotFoundException("Cookbook not found!");
        }
        
        if (cookbook != null && cookbook.ChefId != chefId)
        {
            throw new ChefCookbookAccessViolationException("This chef does not have access to this cookbook.");
        }

        return _mapper.Map(cookbook, new CookbookViewModel());
    }

    public async Task<CookbookViewModel?> CreateCookbook(CookbookViewModel cookbook, int chefId)
    {
        var dbCookbook = new Cookbook
        {
            ChefId = chefId
        };

        _mapper.Map(cookbook, dbCookbook);

        dbCookbook.CoverImage = !string.IsNullOrEmpty(cookbook.CoverImageBase64)
            ? Convert.FromBase64String(cookbook.CoverImageBase64)
            : null;
        
        var id = await _cookbookRepository.CreateCookbook(dbCookbook);
        var newlyCreatedCookbook = await _cookbookRepository.GetCookbookById(id);
        return _mapper.Map(newlyCreatedCookbook, new CookbookViewModel());
    }
}