using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("ingredient");
            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder.HasKey(x => x.Id).HasName("pk_ingredient_id");

            builder.Property(x => x.RecipeId).HasColumnName("recipe_id");

            builder.Property(x => x.Measurement).IsRequired();

            builder.Property(x => x.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired()
                .HasDefaultValueSql("now()");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);

            builder.HasOne(x => x.Recipe).WithMany(x => x.Ingredients).HasForeignKey(x => x.RecipeId);
        }
    }
}
