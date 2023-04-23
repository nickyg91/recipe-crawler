using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeCrawler.Data.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyStepIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_recipe_recipe_id",
                schema: "cheffer",
                table: "ingredient");

            migrationBuilder.DropForeignKey(
                name: "fk_ingredient_step_ingredients",
                schema: "cheffer",
                table: "step_ingredient");

            migrationBuilder.DropForeignKey(
                name: "fk_step_step_ingredients",
                schema: "cheffer",
                table: "step_ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "pk_step_ingredient",
                schema: "cheffer",
                table: "step_ingredient");

            migrationBuilder.RenameTable(
                name: "step_ingredient",
                schema: "cheffer",
                newName: "StepIngredients",
                newSchema: "cheffer");

            migrationBuilder.RenameColumn(
                name: "recipe_id",
                schema: "cheffer",
                table: "ingredient",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_ingredient_recipe_id",
                schema: "cheffer",
                table: "ingredient",
                newName: "IX_ingredient_RecipeId");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "cheffer",
                table: "StepIngredients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "step_id",
                schema: "cheffer",
                table: "StepIngredients",
                newName: "StepId");

            migrationBuilder.RenameColumn(
                name: "ingredient_id",
                schema: "cheffer",
                table: "StepIngredients",
                newName: "IngredientId");

            migrationBuilder.RenameColumn(
                name: "created_at_utc",
                schema: "cheffer",
                table: "StepIngredients",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameIndex(
                name: "IX_step_ingredient_step_id",
                schema: "cheffer",
                table: "StepIngredients",
                newName: "IX_StepIngredients_StepId");

            migrationBuilder.RenameIndex(
                name: "IX_step_ingredient_ingredient_id",
                schema: "cheffer",
                table: "StepIngredients",
                newName: "IX_StepIngredients_IngredientId");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                schema: "cheffer",
                table: "ingredient",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "step_id",
                schema: "cheffer",
                table: "ingredient",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "email_verification_guid",
                schema: "cheffer",
                table: "chef",
                type: "uuid",
                maxLength: 64,
                nullable: true,
                defaultValue: new Guid("0b4906b2-7329-4a5b-93b5-39a50f391351"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 64,
                oldNullable: true,
                oldDefaultValue: new Guid("1bf37d90-4e99-431f-8675-ef2be525a8f0"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAtUtc",
                schema: "cheffer",
                table: "StepIngredients",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StepIngredients",
                schema: "cheffer",
                table: "StepIngredients",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_step_id",
                schema: "cheffer",
                table: "ingredient",
                column: "step_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_recipe_RecipeId",
                schema: "cheffer",
                table: "ingredient",
                column: "RecipeId",
                principalSchema: "cheffer",
                principalTable: "recipe",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_step_step_id",
                schema: "cheffer",
                table: "ingredient",
                column: "step_id",
                principalSchema: "cheffer",
                principalTable: "step",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StepIngredients_ingredient_IngredientId",
                schema: "cheffer",
                table: "StepIngredients",
                column: "IngredientId",
                principalSchema: "cheffer",
                principalTable: "ingredient",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StepIngredients_step_StepId",
                schema: "cheffer",
                table: "StepIngredients",
                column: "StepId",
                principalSchema: "cheffer",
                principalTable: "step",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_recipe_RecipeId",
                schema: "cheffer",
                table: "ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_ingredient_step_step_id",
                schema: "cheffer",
                table: "ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_StepIngredients_ingredient_IngredientId",
                schema: "cheffer",
                table: "StepIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_StepIngredients_step_StepId",
                schema: "cheffer",
                table: "StepIngredients");

            migrationBuilder.DropIndex(
                name: "IX_ingredient_step_id",
                schema: "cheffer",
                table: "ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StepIngredients",
                schema: "cheffer",
                table: "StepIngredients");

            migrationBuilder.DropColumn(
                name: "step_id",
                schema: "cheffer",
                table: "ingredient");

            migrationBuilder.RenameTable(
                name: "StepIngredients",
                schema: "cheffer",
                newName: "step_ingredient",
                newSchema: "cheffer");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                schema: "cheffer",
                table: "ingredient",
                newName: "recipe_id");

            migrationBuilder.RenameIndex(
                name: "IX_ingredient_RecipeId",
                schema: "cheffer",
                table: "ingredient",
                newName: "IX_ingredient_recipe_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "cheffer",
                table: "step_ingredient",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StepId",
                schema: "cheffer",
                table: "step_ingredient",
                newName: "step_id");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                schema: "cheffer",
                table: "step_ingredient",
                newName: "ingredient_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                schema: "cheffer",
                table: "step_ingredient",
                newName: "created_at_utc");

            migrationBuilder.RenameIndex(
                name: "IX_StepIngredients_StepId",
                schema: "cheffer",
                table: "step_ingredient",
                newName: "IX_step_ingredient_step_id");

            migrationBuilder.RenameIndex(
                name: "IX_StepIngredients_IngredientId",
                schema: "cheffer",
                table: "step_ingredient",
                newName: "IX_step_ingredient_ingredient_id");

            migrationBuilder.AlterColumn<int>(
                name: "recipe_id",
                schema: "cheffer",
                table: "ingredient",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "email_verification_guid",
                schema: "cheffer",
                table: "chef",
                type: "uuid",
                maxLength: 64,
                nullable: true,
                defaultValue: new Guid("1bf37d90-4e99-431f-8675-ef2be525a8f0"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 64,
                oldNullable: true,
                oldDefaultValue: new Guid("0b4906b2-7329-4a5b-93b5-39a50f391351"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at_utc",
                schema: "cheffer",
                table: "step_ingredient",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "pk_step_ingredient",
                schema: "cheffer",
                table: "step_ingredient",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ingredient_recipe_recipe_id",
                schema: "cheffer",
                table: "ingredient",
                column: "recipe_id",
                principalSchema: "cheffer",
                principalTable: "recipe",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_ingredient_step_ingredients",
                schema: "cheffer",
                table: "step_ingredient",
                column: "ingredient_id",
                principalSchema: "cheffer",
                principalTable: "ingredient",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_step_step_ingredients",
                schema: "cheffer",
                table: "step_ingredient",
                column: "step_id",
                principalSchema: "cheffer",
                principalTable: "step",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
