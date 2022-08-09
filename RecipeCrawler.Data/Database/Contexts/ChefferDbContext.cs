using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Data.EntityConfigurations;

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
            _chefConfiguration.CreateEntityModel(builder);
            _cookbookConfiguration.CreateEntityModel(builder);
            _recipeConfiguration.CreateEntityModel(builder);
            _stepConfiguration.CreateEntityModel(builder);
            _ingredientConfiguration.CreateEntityModel(builder);
        }
    }
}
