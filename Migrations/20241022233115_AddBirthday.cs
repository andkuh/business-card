using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessCard.Migrations
{
    public partial class AddBirthday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearsOld",
                table: "People");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "People",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "People");

            migrationBuilder.AddColumn<int>(
                name: "YearsOld",
                table: "People",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
