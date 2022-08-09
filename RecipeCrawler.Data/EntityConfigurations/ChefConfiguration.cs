using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations
{
    public class ChefConfiguration : BaseConfiguration
    {
        public override void CreateEntityModel(ModelBuilder builder)
        {
            builder
                .Entity<Chef>(chef =>
                {
                    chef.ToTable("chef");

                    chef
                        .Property(x => x.Id)
                        .HasColumnName("id");

                    chef.HasKey(x => x.Id).HasName("pk_chef_id");

                    chef.Property(x => x.CreatedAtUtc)
                        .HasColumnName("created_at_utc")
                        .IsRequired()
                        .HasDefaultValueSql("now()");

                    chef.Property(x => x.Username)
                        .HasColumnName("username")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true);

                    chef.Property(x => x.PasswordHash)
                        .HasColumnName("password_hash")
                        .HasMaxLength(512);

                    chef.Property(x => x.Email)
                        .HasColumnName("email")
                        .HasMaxLength(512)
                        .IsUnicode(true);
                });
        }
    }
}
