using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations
{
    public class CookbookConfiguration : IEntityTypeConfiguration<Cookbook>
    {
        public void Configure(EntityTypeBuilder<Cookbook> builder)
        {
            builder.ToTable("cookbook");
            builder.Property(x => x.Id)
                        .HasColumnName("id");

            builder.Property(x => x.ChefId).HasColumnName("chef_id");

            builder
                .Property(x => x.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(true);

            builder.Property(x => x.CreatedAtUtc)
                        .HasColumnName("created_at_utc")
                        .IsRequired()
                        .HasDefaultValueSql("now()");
            builder
                .HasOne(x => x.Chef)
                    .WithMany(x => x.Cookbooks)
                    .HasConstraintName("fk_cookbook_chef")
                    .HasForeignKey(x => x.ChefId);
        }
    }
}
