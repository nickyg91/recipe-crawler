using Microsoft.EntityFrameworkCore;
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

        public async Task<Chef?> CheckIfUsernameOrEmailAlreadyExists(string username, string email)
        {
            var chef = await _context.Chefs.FirstOrDefaultAsync(x => x.Username == username);
            if (chef == null)
            {
                chef = await _context.Chefs.FirstOrDefaultAsync(x => x.Email == email);
            }
            return chef;
        }

        public async Task<Chef?> FindChefByEmailAndPassword(string email, string password)
        {
            return await _context.Chefs.FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == password);
        }

        public async Task<Chef> InsertChef(Chef chefToCreate)
        {
            await _context.Chefs.AddAsync(chefToCreate);
            await _context.SaveChangesAsync();
            return chefToCreate;
        }
    }
}
