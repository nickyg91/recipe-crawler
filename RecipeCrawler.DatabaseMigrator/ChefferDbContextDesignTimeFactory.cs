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
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                       .AddJsonFile($"appsettings.json", true, true)
                       .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                       .AddEnvironmentVariables();
            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("cheffer");
            DbContextOptionsBuilder<ChefferDbContext> optionsBuilder = new DbContextOptionsBuilder<ChefferDbContext>()
                .UseNpgsql(connectionString, builder =>
                {
                    builder.MigrationsAssembly("RecipeCrawler.DatabaseMigrator");
                });
            return new ChefferDbContext(optionsBuilder.Options);
        }
    }
}
