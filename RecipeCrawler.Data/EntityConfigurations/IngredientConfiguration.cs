using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations
{
    public class IngredientConfiguration : BaseConfiguration
    {
        public override void CreateEntityModel(ModelBuilder builder)
        {
            builder.Entity<Ingredient>(ingredient =>
            {
                ingredient.ToTable("ingredient");
                ingredient
                    .Property(x => x.Id)
                    .HasColumnName("id");

                ingredient.HasKey(x => x.Id).HasName("pk_ingredient_id");

                ingredient.Property(x => x.RecipeId).HasColumnName("recipe_id");

                ingredient.Property(x => x.Measurement).IsRequired();

                ingredient.Property(x => x.CreatedAtUtc)
                    .HasColumnName("created_at_utc")
                    .IsRequired()
                    .HasDefaultValueSql("now()");

                ingredient.Property(x => x.Name).IsRequired().HasMaxLength(256);

                ingredient.HasOne(x => x.Recipe).WithMany(x => x.Ingredients).HasForeignKey(x => x.RecipeId);
            });
        }
    }
}
