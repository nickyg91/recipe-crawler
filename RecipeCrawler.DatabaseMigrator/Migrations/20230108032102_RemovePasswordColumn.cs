﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeCrawler.DatabaseMigrator.Migrations
{
    public partial class RemovePasswordColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                schema: "cheffer",
                table: "chef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "cheffer",
                table: "chef",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
