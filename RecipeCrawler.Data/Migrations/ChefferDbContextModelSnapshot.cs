﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RecipeCrawler.Data.Database.Contexts;

#nullable disable

namespace RecipeCrawler.Data.Migrations
{
    [DbContext(typeof(ChefferDbContext))]
    partial class ChefferDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("cheffer")
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RecipeCrawler.Entities.Chef", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(512)
                        .IsUnicode(true)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("email");

                    b.Property<Guid?>("EmailVerificationGuid")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(64)
                        .HasColumnType("uuid")
                        .HasDefaultValue(new Guid("df3b7563-849c-463b-b703-ed2dde2db9c6"))
                        .HasColumnName("email_verification_guid");

                    b.Property<bool>("IsEmailVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_email_verified");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("password_hash");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_chef_id");

                    b.ToTable("chef", "cheffer");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Cookbook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChefId")
                        .HasColumnType("integer")
                        .HasColumnName("chef_id");

                    b.Property<byte[]>("CoverImage")
                        .HasMaxLength(5000000)
                        .HasColumnType("bytea")
                        .HasColumnName("cover_image");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_cookbook_id");

                    b.HasIndex("ChefId");

                    b.ToTable("cookbook", "cheffer");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte>("Amount")
                        .HasColumnType("smallint")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc")
                        .HasDefaultValueSql("now()");

                    b.Property<byte>("Measurement")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer")
                        .HasColumnName("recipe_id");

                    b.HasKey("Id")
                        .HasName("pk_ingredient_id");

                    b.HasIndex("RecipeId");

                    b.ToTable("ingredient", "cheffer");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CookbookId")
                        .HasColumnType("integer")
                        .HasColumnName("cookbook_id");

                    b.Property<string>("CrawledHtml")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("crawled_html");

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_recipe_id");

                    b.HasIndex("CookbookId");

                    b.ToTable("recipe", "cheffer");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Step", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer")
                        .HasColumnName("recipe_id");

                    b.HasKey("Id")
                        .HasName("pk_step_id");

                    b.HasIndex("RecipeId");

                    b.ToTable("step", "cheffer");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.StepIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAtUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer")
                        .HasColumnName("ingredient_id");

                    b.Property<int>("StepId")
                        .HasColumnType("integer")
                        .HasColumnName("step_id");

                    b.HasKey("Id")
                        .HasName("pk_step_ingredient");

                    b.HasIndex("IngredientId");

                    b.HasIndex("StepId");

                    b.ToTable("step_ingredient", "cheffer");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Cookbook", b =>
                {
                    b.HasOne("RecipeCrawler.Entities.Chef", "Chef")
                        .WithMany("Cookbooks")
                        .HasForeignKey("ChefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_cookbook_chef");

                    b.Navigation("Chef");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Ingredient", b =>
                {
                    b.HasOne("RecipeCrawler.Entities.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Recipe", b =>
                {
                    b.HasOne("RecipeCrawler.Entities.Cookbook", "Cookbook")
                        .WithMany("Recipes")
                        .HasForeignKey("CookbookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_recipe_cookbook");

                    b.Navigation("Cookbook");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Step", b =>
                {
                    b.HasOne("RecipeCrawler.Entities.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.StepIngredient", b =>
                {
                    b.HasOne("RecipeCrawler.Entities.Ingredient", "Ingredient")
                        .WithMany("StepIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_ingredient_step_ingredients");

                    b.HasOne("RecipeCrawler.Entities.Step", "Step")
                        .WithMany("StepIngredients")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_step_step_ingredients");

                    b.Navigation("Ingredient");

                    b.Navigation("Step");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Chef", b =>
                {
                    b.Navigation("Cookbooks");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Cookbook", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Ingredient", b =>
                {
                    b.Navigation("StepIngredients");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("RecipeCrawler.Entities.Step", b =>
                {
                    b.Navigation("StepIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}
