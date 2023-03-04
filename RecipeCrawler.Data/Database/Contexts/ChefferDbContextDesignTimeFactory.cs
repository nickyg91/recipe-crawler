using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RecipeCrawler.Data.Database.Contexts
{
    public class ChefferDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ChefferDbContext>
    {
        public ChefferDbContext CreateDbContext(string[] args)
        {
            
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json")
                       .AddEnvironmentVariables();
            var configuration = builder.Build();
            var connectionString = ConfigurationExtensions.GetConnectionString(configuration, "cheffer");
            if (environmentName != "Development")
            {
                connectionString = Environment.GetEnvironmentVariable("CHEFFER_CONNECTION_STRING");
            }
            DbContextOptionsBuilder<ChefferDbContext> optionsBuilder = new DbContextOptionsBuilder<ChefferDbContext>()
                .UseNpgsql(connectionString);
            return new ChefferDbContext(optionsBuilder.Options);
        }
    }
}
