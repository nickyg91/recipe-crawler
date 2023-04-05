using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RecipeCrawler.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStepIngredients_JumpTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_step_step_id",
                schema: "cheffer",
                table: "ingredient");

            migrationBuilder.RenameColumn(
                name: "step_id",
                schema: "cheffer",
                table: "ingredient",
                newName: "recipe_id");

            migrationBuilder.RenameIndex(
                name: "IX_ingredient_step_id",
                schema: "cheffer",
                table: "ingredient",
                newName: "IX_ingredient_recipe_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "email_verification_guid",
                schema: "cheffer",
                table: "chef",
                type: "uuid",
                maxLength: 64,
                nullable: true,
                defaultValue: new Guid("1f1a1bc3-4504-4201-af2e-5305d0d8759a"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 64,
                oldNullable: true,
                oldDefaultValue: new Guid("042a70e8-27c4-48a7-82e8-5acf86e973e9"));

            migrationBuilder.CreateTable(
                name: "step_ingredient",
                schema: "cheffer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ingredient_id = table.Column<int>(type: "integer", nullable: false),
                    step_id = table.Column<int>(type: "integer", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_step_ingredient", x => x.id);
                    table.ForeignKey(
                        name: "fk_ingredient_step_ingredients",
                        column: x => x.ingredient_id,
                        principalSchema: "cheffer",
                        principalTable: "ingredient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_step_step_ingredients",
                        column: x => x.step_id,
                        principalSchema: "cheffer",
                        principalTable: "step",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_step_ingredient_ingredient_id",
                schema: "cheffer",
                table: "step_ingredient",
                column: "ingredient_id");

            migrationBuilder.CreateIndex(
                name: "IX_step_ingredient_step_id",
                schema: "cheffer",
                table: "step_ingredient",
                column: "step_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_recipe_recipe_id",
                schema: "cheffer",
                table: "ingredient",
                column: "recipe_id",
                principalSchema: "cheffer",
                principalTable: "recipe",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_recipe_recipe_id",
                schema: "cheffer",
                table: "ingredient");

            migrationBuilder.DropTable(
                name: "step_ingredient",
                schema: "cheffer");

            migrationBuilder.RenameColumn(
                name: "recipe_id",
                schema: "cheffer",
                table: "ingredient",
                newName: "step_id");

            migrationBuilder.RenameIndex(
                name: "IX_ingredient_recipe_id",
                schema: "cheffer",
                table: "ingredient",
                newName: "IX_ingredient_step_id");

            migrationBuilder.AlterColumn<Guid>(
                name: "email_verification_guid",
                schema: "cheffer",
                table: "chef",
                type: "uuid",
                maxLength: 64,
                nullable: true,
                defaultValue: new Guid("042a70e8-27c4-48a7-82e8-5acf86e973e9"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 64,
                oldNullable: true,
                oldDefaultValue: new Guid("1f1a1bc3-4504-4201-af2e-5305d0d8759a"));

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_step_step_id",
                schema: "cheffer",
                table: "ingredient",
                column: "step_id",
                principalSchema: "cheffer",
                principalTable: "step",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
