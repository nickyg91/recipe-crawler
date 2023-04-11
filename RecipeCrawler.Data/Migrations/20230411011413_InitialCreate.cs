using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RecipeCrawler.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cheffer");

            migrationBuilder.CreateTable(
                name: "chef",
                schema: "cheffer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    email = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    email_verification_guid = table.Column<Guid>(type: "uuid", maxLength: 64, nullable: true, defaultValue: new Guid("81503cc6-c52c-4e76-b7ef-9ed65ecad072")),
                    is_email_verified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chef_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cookbook",
                schema: "cheffer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    chef_id = table.Column<int>(type: "integer", nullable: false),
                    cover_image = table.Column<byte[]>(type: "bytea", maxLength: 5000000, nullable: true),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cookbook_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_cookbook_chef",
                        column: x => x.chef_id,
                        principalSchema: "cheffer",
                        principalTable: "chef",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recipe",
                schema: "cheffer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    crawled_html = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    cookbook_id = table.Column<int>(type: "integer", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recipe_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_recipe_cookbook",
                        column: x => x.cookbook_id,
                        principalSchema: "cheffer",
                        principalTable: "cookbook",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ingredient",
                schema: "cheffer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Measurement = table.Column<byte>(type: "smallint", nullable: false),
                    amount = table.Column<byte>(type: "smallint", nullable: false),
                    recipe_id = table.Column<int>(type: "integer", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ingredient_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_ingredient_recipe_recipe_id",
                        column: x => x.recipe_id,
                        principalSchema: "cheffer",
                        principalTable: "recipe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "step",
                schema: "cheffer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    recipe_id = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_step_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_step_recipe_recipe_id",
                        column: x => x.recipe_id,
                        principalSchema: "cheffer",
                        principalTable: "recipe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_cookbook_chef_id",
                schema: "cheffer",
                table: "cookbook",
                column: "chef_id");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_recipe_id",
                schema: "cheffer",
                table: "ingredient",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_cookbook_id",
                schema: "cheffer",
                table: "recipe",
                column: "cookbook_id");

            migrationBuilder.CreateIndex(
                name: "IX_step_recipe_id",
                schema: "cheffer",
                table: "step",
                column: "recipe_id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "step_ingredient",
                schema: "cheffer");

            migrationBuilder.DropTable(
                name: "ingredient",
                schema: "cheffer");

            migrationBuilder.DropTable(
                name: "step",
                schema: "cheffer");

            migrationBuilder.DropTable(
                name: "recipe",
                schema: "cheffer");

            migrationBuilder.DropTable(
                name: "cookbook",
                schema: "cheffer");

            migrationBuilder.DropTable(
                name: "chef",
                schema: "cheffer");
        }
    }
}
