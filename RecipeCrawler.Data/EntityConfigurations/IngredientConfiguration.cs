﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeCrawler.Entities;

namespace RecipeCrawler.Data.EntityConfigurations;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.ToTable("ingredient");
        builder
            .Property(x => x.Id)
            .UseIdentityColumn()
            .HasColumnName("id");

        builder.HasKey(x => x.Id).HasName("pk_ingredient_id");

        builder.Property(x => x.Measurement).IsRequired();
        builder.Property(x => x.StepId).IsRequired().HasColumnName("step_id");
        builder.Property(x => x.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);

        builder.Property(x => x.Amount).HasColumnName("amount").IsRequired();

        builder
            .HasOne(x => x.Step)
            .WithMany(x => x.Ingredients)
            .HasForeignKey(x => x.StepId);
    }
}