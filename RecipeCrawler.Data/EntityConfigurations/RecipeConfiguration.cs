using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("recipe");
            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder.HasKey(x => x.Id).HasName("pk_recipe_id");

            builder.Property(x => x.CrawledHtml).HasColumnName("crawled_html").HasMaxLength(2048);
            builder.Property(x => x.CreatedAtUtc)
                    .HasColumnName("created_at_utc")
                    .IsRequired()
                    .HasDefaultValueSql("now()");

            builder.Property(x => x.CookbookId).HasColumnName("cookbook_id");

            builder.HasOne<Cookbook>()
            .WithMany(x => x.Recipes)
            .HasConstraintName("fk_recipe_cookbook")
            .HasForeignKey(x => x.CookbookId);

            builder.HasMany<Ingredient>().WithOne(x => x.Recipe);
            builder.HasMany<Step>().WithOne(x => x.Recipe);
        }
    }
}
