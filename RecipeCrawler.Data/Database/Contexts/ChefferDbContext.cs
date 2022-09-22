using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Data.EntityConfigurations;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Database.Contexts
{
    public class ChefferDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly CookbookConfiguration _cookbookConfiguration;
        private readonly ChefConfiguration _chefConfiguration;
        private readonly RecipeConfiguration _recipeConfiguration;
        private readonly StepConfiguration _stepConfiguration;
        private readonly IngredientConfiguration _ingredientConfiguration;

        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Cookbook> Cookbooks { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        //explicitly used for ef migrations
        public ChefferDbContext(DbContextOptions<ChefferDbContext> options)
        : base(options)
        {
            _cookbookConfiguration = new CookbookConfiguration();
            _chefConfiguration = new ChefConfiguration();
            _recipeConfiguration = new RecipeConfiguration();
            _stepConfiguration = new StepConfiguration();
            _ingredientConfiguration = new IngredientConfiguration();
        }

        public ChefferDbContext(string connectionString,
            CookbookConfiguration cookbookConfiguration,
            ChefConfiguration chefConfiguration,
            RecipeConfiguration recipeConfiguration,
            StepConfiguration stepConfiguration,
            IngredientConfiguration ingredientConfiguration) : base()
        {
            _connectionString = connectionString;
            _cookbookConfiguration = cookbookConfiguration;
            _chefConfiguration = chefConfiguration;
            _recipeConfiguration = recipeConfiguration;
            _stepConfiguration = stepConfiguration;
            _ingredientConfiguration = ingredientConfiguration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseNpgsql(_connectionString, builder =>
                {
                    builder.MigrationsAssembly("RecipeCrawler.DatabaseMigrator");
                });
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("cheffer");
            _chefConfiguration.Configure(builder.Entity<Chef>());
            _cookbookConfiguration.Configure(builder.Entity<Cookbook>());
            _recipeConfiguration.Configure(builder.Entity<Recipe>());
            _stepConfiguration.Configure(builder.Entity<Step>());
            _ingredientConfiguration.Configure(builder.Entity<Ingredient>());
        }
    }
}
