using RecipeCrawler.Data.Database.Contexts;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Repositories.Implementations
{
    public class ChefRepository : IChefRepository
    {
        private ChefferDbContext _context;
        public ChefRepository(ChefferDbContext context)
        {
            _context = context;
        }

        public async Task<Chef> InsertChef(Chef chefToCreate)
        {
            await _context.Chefs.AddAsync(chefToCreate);
            await _context.SaveChangesAsync();
            return chefToCreate;
        }
    }
}
