using Microsoft.EntityFrameworkCore;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.Database.Contexts.Cookbookr
{
    public class CookbookrDbContext : DbContext
    {
        private readonly string _connectionString;
        public CookbookrDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("cookbookr");
            builder
                .Entity<Chef>(chef =>
                {
                    chef
                        .Property(x => x.Id)
                        .HasColumnName("id")
                        .UseIdentityColumn();
                    chef.HasKey(x => x.Id).HasName("pk_chef_id");

                    chef.Property(x => x.CreatedAtUtc)
                        .HasColumnName("created_at_utc")
                        .IsRequired()
                        .HasDefaultValue(DateTime.UtcNow);

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

            builder.Entity<Cookbook>(cookbook =>
            {
                cookbook.HasOne(x => x.Chef).WithMany(x => x.Cookbooks).HasConstraintName("fk_cookbook_chef");

                cookbook.Property(x => x.Id)
                        .HasColumnName("id")
                        .UseIdentityColumn();

                cookbook
                    .Property(x => x.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(true);
            });
                
        }
    }
}
