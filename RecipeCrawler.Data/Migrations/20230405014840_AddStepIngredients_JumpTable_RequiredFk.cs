using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeCrawler.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStepIngredients_JumpTable_RequiredFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "email_verification_guid",
                schema: "cheffer",
                table: "chef",
                type: "uuid",
                maxLength: 64,
                nullable: true,
                defaultValue: new Guid("df3b7563-849c-463b-b703-ed2dde2db9c6"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 64,
                oldNullable: true,
                oldDefaultValue: new Guid("1f1a1bc3-4504-4201-af2e-5305d0d8759a"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                oldDefaultValue: new Guid("df3b7563-849c-463b-b703-ed2dde2db9c6"));
        }
    }
}
