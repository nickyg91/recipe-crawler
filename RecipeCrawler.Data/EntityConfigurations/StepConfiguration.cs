using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations
{
    public class StepConfiguration : IEntityTypeConfiguration<Step>
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.ToTable("step");

            builder.HasKey(x => x.Id).HasName("pk_step_id");

            builder
                .Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnName("id");

            builder.Property(x => x.RecipeId).HasColumnName("recipe_id");

            builder.Property(x => x.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired()
                .HasDefaultValueSql("now()");

            builder.Property(x => x.Description).IsRequired().HasMaxLength(256);

            builder
                .HasOne(x => x.Recipe)
                .WithMany(x => x.Steps)
                .HasForeignKey(x => x.RecipeId);
        }
    }
}
