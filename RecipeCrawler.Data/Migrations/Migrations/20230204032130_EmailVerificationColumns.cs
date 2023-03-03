#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeCrawler.Data.Migrations.Migrations
{
    public partial class EmailVerificationColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "email_verification_guid",
                schema: "cheffer",
                table: "chef",
                type: "uuid",
                maxLength: 64,
                nullable: true,
                defaultValue: new Guid("6c1e2196-7ab5-4be4-ac7e-abe7098e3ad4"));

            migrationBuilder.AddColumn<bool>(
                name: "is_email_verified",
                schema: "cheffer",
                table: "chef",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_verification_guid",
                schema: "cheffer",
                table: "chef");

            migrationBuilder.DropColumn(
                name: "is_email_verified",
                schema: "cheffer",
                table: "chef");
        }
    }
}
