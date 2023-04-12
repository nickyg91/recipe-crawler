using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeCrawler.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUseIdentityToIdColumns : Migration
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
                defaultValue: new Guid("91ef49cc-bfbf-47ac-8e20-d8f79989fe4b"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 64,
                oldNullable: true,
                oldDefaultValue: new Guid("81503cc6-c52c-4e76-b7ef-9ed65ecad072"));
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
                defaultValue: new Guid("81503cc6-c52c-4e76-b7ef-9ed65ecad072"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 64,
                oldNullable: true,
                oldDefaultValue: new Guid("91ef49cc-bfbf-47ac-8e20-d8f79989fe4b"));
        }
    }
}
