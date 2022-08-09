using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations
{
    public class StepConfiguration : BaseConfiguration
    {
        public override void CreateEntityModel(ModelBuilder builder)
        {
            builder.Entity<Step>(step =>
            {
                step.ToTable("step");

                step.HasKey(x => x.Id).HasName("pk_step_id");

                step
                    .Property(x => x.Id)
                    .HasColumnName("id");

                step.Property(x => x.RecipeId).HasColumnName("recipe_id");

                step.Property(x => x.CreatedAtUtc)
                    .HasColumnName("created_at_utc")
                    .IsRequired()
                    .HasDefaultValueSql("now()");

                step.Property(x => x.Description).IsRequired().HasMaxLength(256);

                step.HasOne(x => x.Recipe).WithMany(x => x.Steps).HasForeignKey(x => x.RecipeId);
            });
        }
    }
}
