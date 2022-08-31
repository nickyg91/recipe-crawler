﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations
{
    public class ChefConfiguration : IEntityTypeConfiguration<Chef>
    {
        public void Configure(EntityTypeBuilder<Chef> builder)
        {
            builder.ToTable("chef");

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder.HasKey(x => x.Id).HasName("pk_chef_id");

            builder.Property(x => x.CreatedAtUtc)
                .HasColumnName("created_at_utc")
                .IsRequired()
                .HasDefaultValueSql("now()");

            builder.Property(x => x.Username)
                .HasColumnName("username")
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(true);

            builder.Property(x => x.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(512);

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(512)
                .IsUnicode(true);
        }
    }
}