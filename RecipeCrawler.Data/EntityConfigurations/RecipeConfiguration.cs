using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations
{
    public class RecipeConfiguration : BaseConfiguration
    {
        public override void CreateEntityModel(ModelBuilder builder)
        {
            builder.Entity<Recipe>(recipe =>
            {
                recipe.ToTable("recipe");
                recipe
                    .Property(x => x.Id)
                    .HasColumnName("id");

                recipe.HasKey(x => x.Id).HasName("pk_recipe_id");

                recipe.Property(x => x.CrawledHtml).HasColumnName("crawled_html").HasMaxLength(2048);
                recipe.Property(x => x.CreatedAtUtc)
                        .HasColumnName("created_at_utc")
                        .IsRequired()
                        .HasDefaultValueSql("now()");

                recipe.Property(x => x.CookbookId).HasColumnName("cookbook_id");

                recipe.HasOne<Cookbook>()
                .WithMany(x => x.Recipes)
                .HasConstraintName("fk_recipe_cookbook")
                .HasForeignKey(x => x.CookbookId);

                recipe.HasMany<Ingredient>().WithOne(x => x.Recipe);
                recipe.HasMany<Step>().WithOne(x => x.Recipe);
            });
        }
    }
}
