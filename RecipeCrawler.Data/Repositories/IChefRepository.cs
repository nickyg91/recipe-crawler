using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Repositories
{
    public interface IChefRepository
    {
        Task<Chef> InsertChef(Chef chefToCreate);
    }
}
