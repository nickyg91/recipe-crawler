using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Data.Database.Contexts;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Repositories.Implementations
{
    public class ChefRepository : IChefRepository
    {
        private readonly ChefferDbContext _context;
        public ChefRepository(ChefferDbContext context)
        {
            _context = context;
        }

        public async Task<Chef?> CheckIfUsernameOrEmailAlreadyExists(string username, string email)
        {
            var chef = await _context.Chefs.FirstOrDefaultAsync(x => x.Username == username) ?? await _context.Chefs.FirstOrDefaultAsync(x => x.Email == email);
            return chef;
        }

        public async Task<Chef?> FindChefByUsername(string username)
        {
            return await _context.Chefs.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<Chef?> FindChefByVerificationGuid(Guid verificationGuid)
        {
            return await _context.Chefs.SingleOrDefaultAsync(x => x.EmailVerificationGuid == verificationGuid);
        }

        public async Task<bool> VerifyChef(Chef chef)
        {
            if (!chef.IsEmailVerified)
            {
                if (chef.CreatedAtUtc.AddDays(3) > DateTime.UtcNow)
                {
                    chef.IsEmailVerified = true;
                    chef.EmailVerificationGuid = null;
                }
            }

            await _context.SaveChangesAsync();
            return chef.IsEmailVerified;
        }

        public async Task<Chef?> ResetVerificationGuid(Guid guid, string email)
        {
            var chef = await _context.Chefs.SingleOrDefaultAsync(x =>
                x.Email == email && x.EmailVerificationGuid == guid);
            if (chef == null) return chef;
            chef.EmailVerificationGuid = Guid.NewGuid();
            await _context.SaveChangesAsync();

            return chef;
        }

        public async Task<Chef?> GetChefByVerificationGuid(Guid guid)
        {
            var chef = await _context.Chefs.SingleOrDefaultAsync(x => x.EmailVerificationGuid == guid);
            return chef;
        }

        public async Task<Chef> InsertChef(Chef chefToCreate)
        {
            await _context.Chefs.AddAsync(chefToCreate);
            await _context.SaveChangesAsync();
            return chefToCreate;
        }
    }
}
