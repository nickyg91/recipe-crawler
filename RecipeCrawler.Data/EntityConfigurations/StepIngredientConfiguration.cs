using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations;

public class StepIngredientConfiguration : IEntityTypeConfiguration<StepIngredient>
{
    public void Configure(EntityTypeBuilder<StepIngredient> builder)
    {
        builder.ToTable("step_ingredient");
        builder.Property(x => x.Id).HasColumnName("id");
        builder.HasKey(x => x.Id).HasName("pk_step_ingredient");

        builder
            .Property(x => x.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .HasDefaultValueSql("now()");

        builder.Property(x => x.StepId).HasColumnName("step_id").IsRequired();
        builder.Property(x => x.IngredientId).HasColumnName("ingredient_id").IsRequired();

        builder.HasOne(x => x.Step)
            .WithMany(x => x.StepIngredients)
            .HasForeignKey(x => x.StepId)
            .IsRequired()
            .HasConstraintName("fk_step_step_ingredients");
        
        builder.HasOne(x => x.Ingredient)
            .WithMany(x => x.StepIngredients)
            .HasForeignKey(x => x.IngredientId)
            .IsRequired()
            .HasConstraintName("fk_ingredient_step_ingredients");
    }
}