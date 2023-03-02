using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RecipeCrawler.Data.Database.Contexts;

namespace RecipeCrawler.DatabaseMigrator
{
    public class ChefferDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ChefferDbContext>
    {
        public ChefferDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                       .AddEnvironmentVariables();
            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("cheffer");
            DbContextOptionsBuilder<ChefferDbContext> optionsBuilder = new DbContextOptionsBuilder<ChefferDbContext>()
                .UseNpgsql(connectionString, npgsqlBuilder =>
                {
                    npgsqlBuilder.MigrationsAssembly("RecipeCrawler.DatabaseMigrator");
                });
            return new ChefferDbContext(optionsBuilder.Options);
        }
    }
}
