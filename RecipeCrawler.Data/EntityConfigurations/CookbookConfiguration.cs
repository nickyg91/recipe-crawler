using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations
{
    public class CookbookConfiguration : BaseConfiguration
    {
        public override void CreateEntityModel(ModelBuilder builder)
        {
            builder.Entity<Cookbook>(cookbook =>
            {
                cookbook.ToTable("cookbook");
                cookbook.Property(x => x.Id)
                        .HasColumnName("id");

                cookbook.Property(x => x.ChefId).HasColumnName("chef_id");

                cookbook
                    .Property(x => x.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(true);

                cookbook.Property(x => x.CreatedAtUtc)
                        .HasColumnName("created_at_utc")
                        .IsRequired()
                        .HasDefaultValueSql("now()");
                cookbook
                    .HasOne(x => x.Chef)
                    .WithMany(x => x.Cookbooks)
                    .HasConstraintName("fk_cookbook_chef")
                    .HasForeignKey(x => x.ChefId);
            });
        }
    }
}
