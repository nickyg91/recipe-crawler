using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeCrawler.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountToIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "amount",
                schema: "cheffer",
                table: "ingredient",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<Guid>(
                name: "email_verification_guid",
                schema: "cheffer",
                table: "chef",
                type: "uuid",
                maxLength: 64,
                nullable: true,
                defaultValue: new Guid("1a7c23a1-5430-48f3-8bb6-360e7d17fd4c"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 64,
                oldNullable: true,
                oldDefaultValue: new Guid("5d854f3b-467e-443f-8625-e57cb873c6a5"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                schema: "cheffer",
                table: "ingredient");

            migrationBuilder.AlterColumn<Guid>(
                name: "email_verification_guid",
                schema: "cheffer",
                table: "chef",
                type: "uuid",
                maxLength: 64,
                nullable: true,
                defaultValue: new Guid("5d854f3b-467e-443f-8625-e57cb873c6a5"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 64,
                oldNullable: true,
                oldDefaultValue: new Guid("1a7c23a1-5430-48f3-8bb6-360e7d17fd4c"));
        }
    }
}
